// pch.cpp: файл исходного кода, соответствующий предварительно скомпилированному заголовочному файлу

#include "pch.h"

// При использовании предварительно скомпилированных заголовочных файлов необходим следующий файл исходного кода для выполнения сборки.
#include <iostream>
#include "mkl.h"
#include "mkl_vml_functions.h"

void mkl_sin(const int n, double* a, double* y, char c)
{
	MKL_INT64 mode;
	if (c == 'H')
	{
		mode = VML_HA | VML_FTZDAZ_OFF;
	}
	else if (c == 'E')
	{
		mode = VML_EP | VML_FTZDAZ_ON;
	}
	else
	{
		mode = VML_LA | VML_FTZDAZ_ON;
	}
	mode = mode | VML_ERRMODE_DEFAULT;
	vmdSin(n, a, y, mode);
}

void mkl_cos(const int n, double* a, double* y, char c)
{
	MKL_INT64 mode;
	if (c == 'H')
	{
		mode = VML_HA | VML_FTZDAZ_OFF;
	}
	else if (c == 'E')
	{
		mode = VML_EP | VML_FTZDAZ_ON;
	}
	else
	{
		mode = VML_LA | VML_FTZDAZ_ON;
	}
	mode = mode | VML_ERRMODE_DEFAULT;
	vmdCos(n, a, y, mode);
}