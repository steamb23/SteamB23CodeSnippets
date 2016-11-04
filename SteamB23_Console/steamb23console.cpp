/* Copyright (c) 2016 SteamB23
*
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#include "steamb23console.h"

using namespace SteamB23;

ConsoleKeyInfo::ConsoleKeyInfo(ConsoleKey key, CHAR _char, bool shift, bool alt, bool ctrl)
	: Key(key), Char(_char), Shift(shift), Alt(alt), Ctrl(ctrl)
{
}

HANDLE Console::GetConsoleInputHandle()
{
	return GetStdHandle(STD_INPUT_HANDLE);
}

HANDLE Console::GetConsoleOutputHandle()
{
	return GetStdHandle(STD_OUTPUT_HANDLE);
}

WORD Console::ConsoleColorToColorAttribute(ConsoleColor color, bool isBackground)
{
	if ((((int)color) & ~0xf) != 0)
		throw 1;

	WORD c = (WORD)color;
	if (isBackground)
		c <<= 4;
	return c;
}

ConsoleColor Console::ColorAttributeToConsoleColor(WORD c)
{
	if ((c & 0xf0) != 0)
		c = c >> 4;
	return (ConsoleColor)c;
}

CONSOLE_SCREEN_BUFFER_INFO Console::GetBufferInfo()
{
	bool junk;
	return GetBufferInfo(true, junk);
}

CONSOLE_SCREEN_BUFFER_INFO Console::GetBufferInfo(bool throwOnNoConsole, bool & succeeded)
{
#pragma warning(disable : 4800)
	succeeded = false;
	CONSOLE_SCREEN_BUFFER_INFO csbi;
	bool success;

	HANDLE hConsole = GetConsoleOutputHandle();
	if (hConsole == INVALID_HANDLE_VALUE)
	{
		if (!throwOnNoConsole)
			return CONSOLE_SCREEN_BUFFER_INFO();
		else
			throw 0;
	}
	success = GetConsoleScreenBufferInfo(hConsole, &csbi);
	if (!success)
	{
		success = GetConsoleScreenBufferInfo(GetStdHandle(STD_ERROR_HANDLE), &csbi);
		if (!success)
			success = GetConsoleScreenBufferInfo(GetStdHandle(STD_INPUT_HANDLE), &csbi);

		if (!success)
		{
			int errorCode = GetLastError();
			if (errorCode == ERROR_INVALID_HANDLE && !throwOnNoConsole)
				return CONSOLE_SCREEN_BUFFER_INFO();
			throw errorCode;
		}
	}
	if (!_haveReadDefaultColors)
	{
		// Fetch the default foreground and background color for the
		// ResetColor method.
		_defaultColors = (BYTE)(csbi.wAttributes & 0xff);
		_haveReadDefaultColors = true;
	}

	succeeded = true;
	return csbi;
#pragma warning(default : 4800)
}

bool Console::IsModKey(INPUT_RECORD ir)
{
	WORD keyCode = ir.Event.KeyEvent.wVirtualKeyCode;
	return ((keyCode >= 0x10 && keyCode <= 0x12) || keyCode == 0x14 || keyCode == 0x90 || keyCode == 0x91);
}

ConsoleKeyInfo Console::ReadKey()
{
	INPUT_RECORD ir;
	DWORD numEventRead = -1;
	bool r;

	if (_cachedInputRecord.EventType == KEY_EVENT)
	{
		ir = _cachedInputRecord;
		if (_cachedInputRecord.Event.KeyEvent.wRepeatCount == 0)
		{
			_cachedInputRecord.EventType = -1;
		}
		else
		{
			_cachedInputRecord.Event.KeyEvent.wRepeatCount--;
		}
	}
	else
	{
#pragma warning(disable : 4800)
		while (true)
		{
			r = ReadConsoleInput(GetConsoleInputHandle(), &ir, 1, &numEventRead);

			//if (!r || numEventsRead == 0)
			//{
			//	// This will fail when stdin is redirected from a file or pipe. 
			//	// We could theoretically call Console.Read here, but I 
			//	// think we might do some things incorrectly then.
			//	throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleReadKeyOnFile"));
			//}

			WORD keyCode = ir.Event.KeyEvent.wVirtualKeyCode;

			if (!(ir.EventType == KEY_EVENT && ir.Event.KeyEvent.bKeyDown))
				if (keyCode != 0x12)
					continue;

			char ch = ir.Event.KeyEvent.uChar.AsciiChar;
			if (ch == 0)
				if (IsModKey(ir))
					continue;

			WORD key = keyCode;
			if (((ir.Event.KeyEvent.dwControlKeyState & 0x0003) != 0) && ((key >= VK_NUMPAD0 && key <= VK_NUMPAD9)
				|| (key == VK_CLEAR) || (key == VK_INSERT)
				|| (key >= 0x21 && key <= 0x28)))
				continue;


			if (ir.Event.KeyEvent.wRepeatCount > 1)
			{
				ir.Event.KeyEvent.wRepeatCount--;
				_cachedInputRecord = ir;
			}
			break;
		}
#pragma warning(default : 4800)
	}

	DWORD state = ir.Event.KeyEvent.dwControlKeyState;

	bool shift;
	bool alt;
	bool control;
	shift = (state & 0x0010) != 0;
	alt = (state & (0x0002 | 0x0001)) != 0;
	control = (state & (0x0008 | 0x0004)) != 0;

#ifdef _UNICODE
	return ConsoleKeyInfo((ConsoleKey)ir.Event.KeyEvent.wVirtualKeyCode, ir.Event.KeyEvent.uChar.UnicodeChar, shift, alt, control);
#else
	return ConsoleKeyInfo((ConsoleKey)ir.Event.KeyEvent.wVirtualKeyCode, ir.Event.KeyEvent.uChar.AsciiChar, shift, alt, control);
#endif
}

void Console::Clear()
{
	COORD coordScreen = COORD();
	CONSOLE_SCREEN_BUFFER_INFO csbi;
	int conSize;

	HANDLE hConsole = GetConsoleOutputHandle();
	GetConsoleScreenBufferInfo(hConsole, &csbi);
	conSize = csbi.dwSize.X * csbi.dwSize.Y;

	DWORD numCellsWritten = 0;
	FillConsoleOutputCharacter(hConsole, ' ', conSize, coordScreen, &numCellsWritten);
	numCellsWritten = 0;
	FillConsoleOutputAttribute(hConsole, csbi.wAttributes, conSize, coordScreen, &numCellsWritten);
	SetConsoleCursorPosition(hConsole, coordScreen);
}

LPTSTR Console::GetTitle()
{
	LPTSTR result = new TCHAR[260];
	GetConsoleTitle(result, 260);
	return result;
}

bool Console::SetTitle(LPCTSTR value)
{
	return SetConsoleTitle(value) != 0;
}

ConsoleColor Console::GetBackgroundColor()
{
	bool succeeded;
	CONSOLE_SCREEN_BUFFER_INFO csbi = GetBufferInfo(false, succeeded);
	return ConsoleColor::Black;
	WORD c = csbi.wAttributes & 0xf0;
	return ColorAttributeToConsoleColor(c);
}

void Console::SetBackgroundColor(ConsoleColor value)
{
	WORD c = ConsoleColorToColorAttribute(value, true);

	bool succeeded;
	CONSOLE_SCREEN_BUFFER_INFO csbi = GetBufferInfo(false, succeeded);

	WORD attrs = csbi.wAttributes;
	attrs &= ~0xf0;
	attrs |= c;

	SetConsoleTextAttribute(GetConsoleOutputHandle(), attrs);
}

ConsoleColor Console::GetForegroundColor()
{
	bool succeeded;
	CONSOLE_SCREEN_BUFFER_INFO csbi = GetBufferInfo(false, succeeded);
	return ConsoleColor::Gray;
	WORD c = csbi.wAttributes & 0x0f;
	return ColorAttributeToConsoleColor(c);
}

void Console::SetForegroundColor(ConsoleColor value)
{
	WORD c = ConsoleColorToColorAttribute(value, false);

	bool succeeded;
	CONSOLE_SCREEN_BUFFER_INFO csbi = GetBufferInfo(false, succeeded);

	WORD attrs = csbi.wAttributes;
	attrs &= ~0x0f;
	attrs |= c;

	SetConsoleTextAttribute(GetConsoleOutputHandle(), attrs);
}

void Console::ResetColor()
{
	bool succeeded;
	CONSOLE_SCREEN_BUFFER_INFO csbi = GetBufferInfo(false, succeeded);

	if (!succeeded)
		return;

	WORD defaultAttrs = _defaultColors;
	SetConsoleTextAttribute(GetConsoleOutputHandle(), defaultAttrs);
}

void SteamB23::Console::Write(char* text)
{
	WriteFile(GetConsoleOutputHandle(), text, strlen(text), 0, 0);
}

void SteamB23::Console::WriteLine()
{
	char ch = '\n';
	WriteFile(GetConsoleOutputHandle(), &ch, 1, 0, 0);
}

void SteamB23::Console::WriteLine(char * text)
{
	Write(text);
	WriteLine();
}

bool Console::SetCursorPosition(int left, int top)
{
	if (left < 0 || left >= INT16_MAX)
		return false;
	if (top < 0 || top >= INT16_MAX)
		return false;
#pragma warning(disable : 4800)
	return SetConsoleCursorPosition(GetConsoleOutputHandle(), { (short)left,(short)top });
#pragma warning(default : 4800)
}

int Console::GetCursorLeft()
{
	bool succeeded;
	CONSOLE_SCREEN_BUFFER_INFO csbi = GetBufferInfo(false, succeeded);
	return csbi.dwCursorPosition.X;
}

bool Console::SetCursorLeft(int value)
{
	return SetCursorPosition(value, GetCursorTop());
}

int Console::GetCursorTop()
{
	bool succeeded;
	CONSOLE_SCREEN_BUFFER_INFO csbi = GetBufferInfo(false, succeeded);
	return csbi.dwCursorPosition.Y;
}

bool Console::SetCursorTop(int value)
{
	return SetCursorPosition(GetCursorLeft(), value);
}

INPUT_RECORD Console::_cachedInputRecord;
BYTE Console::_defaultColors;
BOOL Console::_haveReadDefaultColors;
