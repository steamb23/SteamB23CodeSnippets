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
