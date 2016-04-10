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

