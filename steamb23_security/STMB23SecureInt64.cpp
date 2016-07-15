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
#include "STMB23SecureInt64.h"
#include "STMB23Security.h"


STMB23SecureInt64::STMB23SecureInt64() : STMB23SecureInt64(0){}

STMB23SecureInt64::STMB23SecureInt64(long value)
{
	this->keyIndex = (char)(value % STMB23Security::keysLength);
	long securedValue = value ^ STMB23Security::keys[keyIndex];
	this->hash = STMB23Security::hashCompute(securedValue);
	this->securedValue = securedValue;
}

STMB23SecureInt64::~STMB23SecureInt64()
{
}

long STMB23SecureInt64::getValue()
{
	if (!isIntegrity())
	{
		this->hash = STMB23Security::hashCompute(STMB23Security::keys[0]);
		this->securedValue = STMB23Security::keys[0];
		this->keyIndex = 0;
	}
	return STMB23Security::decryption(this->securedValue, this->keyIndex);
}

void STMB23SecureInt64::setValue(long value)
{
	if (isIntegrity())
	{
		long secureValue = STMB23Security::encryption(value, &keyIndex);
		this->hash = STMB23Security::hashCompute(secureValue);
		this->securedValue = secureValue;
	}
	else
	{
		this->hash = STMB23Security::hashCompute(STMB23Security::keys[0]);
		this->securedValue = STMB23Security::keys[0];
		this->keyIndex = 0;
	}
}

long STMB23SecureInt64::getRawValue()
{
	return this->securedValue;
}

bool STMB23SecureInt64::isIntegrity()
{
	return this->hash == STMB23Security::hashCompute(this->securedValue);
}

STMB23SecureInt64::operator long()
{
	return this->getValue();
}

