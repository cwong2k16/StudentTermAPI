# Student Term API
---
Simple RESTful API service using ASP.NET Core template.

## Create Endpoint for Current Term
### URL: /term/current
This endpoint should return what term it is based on current date:

September - December: Fall
Jan: Winter
Feb-May: Spring
June-August: Summer

Return: JSON

## Create Endpoint for Term for a Specific Date
### URL: /term/mm-dd-yyyy (datetime)
This endpoint should return what term the provided date is from.

## Create Endpoint for Term from Term Code
### URL: /term/:termcode (int)
This endpoint should return what term the provided term code represents Term codes are 4 digit numbers following the form: 1188  
The first three digits represent the number of years since 1900 (118 + 1900 = 2018) 
The last digit represents the season: 
  1 - Winter
  4 - Spring
  6 - Summer
  8 - Fall
  
## Create Endpoint for Term from Term String
### URL: /term/:term-string (string)
This endpoint should return the term code for the provided term string.
Format: {Term}-{Year}
ex: /term/Fall-2016 will return: 
```
{ 
  "termcode":"1158" 
}
```
