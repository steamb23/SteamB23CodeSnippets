#pragma once
// interface
template<typename T>
struct STMB23ISecureValueType
{
public:
	virtual T getValue() = 0;
	virtual void setValue(T value) = 0;
	virtual long getRawValue() = 0;
	virtual bool isIntegrity() = 0;
};