#pragma once
#include "STMB23ISecureValueType.h"
struct STMB23SecureInt64 :
	public STMB23ISecureValueType<long>
{
public:
	STMB23SecureInt64();
	STMB23SecureInt64(long value);
	~STMB23SecureInt64();

	long getValue();
	void setValue(long value);
	long getRawValue();
	bool isIntegrity();

	operator long();
private:
	char keyIndex = 0;
	long hash = 0;
	long securedValue = 0;
};

