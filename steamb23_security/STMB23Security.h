/* Copyright (c) 2016 SteamB23
*
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#pragma once
namespace STMB23Security {
	const unsigned char keysLength = 16;
	const long keys[keysLength] = {
		6338887092739851599,
		7430565509821446124,
		-9042072405887803858,
		1330224199253267991,
		-7155745344322912499,
		-9089750803117477263,
		3712924929968051920,
		322698015418847168,
		9214776268477700801,
		6896323569820898924,
		1766586946294160572,
		-253872918563961160,
		6507740389827041271,
		1690234834608976180,
		-7814391039444818982,
		6742709491675838358
	};
	long encryption(long value, char * keyIndex);
	long decryption(long value, char keyIndex);

	const long hashSalt = -1703559228456993676;
	long hashCompute(long value);
}