﻿#include <stdio.h>

int main() {
	double w, h;

	printf("체중(kg) : ");
	scanf_s("%lf", &w);

	printf("키(cm) : ");
	scanf_s("%lf", &h);

	double bmi = w / ((h/100) * (h/100));
	printf("BMI = %f:\n", bmi);
}
