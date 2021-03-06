/* Copyright (c) 2016 SteamB23
*
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#include "stdafx.h"
#include "STMB23Security.h"

long STMB23Security::encryption(long value, char * keyIndex)
{
	*keyIndex = (char)(value % keysLength);
	return value ^ keys[*keyIndex];
}

long STMB23Security::decryption(long value, char keyIndex)
{
	return value ^ keys[keyIndex];
}

long STMB23Security::hashCompute(long value)
{
	value ^= -hashSalt;
	value += ~(value << 15);
	value ^= (value >> 10);
	value += (value << 3);
	value ^= (value >> 6);
	value += ~(value << 11);
	value ^= (value >> 16);
	return value;
}
