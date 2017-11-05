#BenjaminHaldin Gambit Challange

## Problem description

TUF-2000M is an ultrasonic energy meter that has a [Modbus](https://en.wikipedia.org/wiki/Modbus) interface described in docs/tuf-2000m.pdf.

Gambit has access to one of these meters and it is providing you a [live text feed](http://tuftuf.gambitlabs.fi/feed.txt) that shows the time of the reading followed by the first 100 register values.

## Challange

The challange was to convert the data and present it in a "human readable" form. And depending on my skills and intrests, create a web service for the converted data.

### Solution

When I figured out how to convert the data with the help of the ModBus interface document and some help from Google, I started a ASP.NET core Web Application using mostly html and C#. I used C# to fetch the data straight from the file provided trough the Url. In the controller I used a streamReader to read the file. The reader read each line while a switch case function was used to get the right registers and then send the data as a string to a ViewData[] variable that can then be read in the frontend (Html). 

#### GetREAL4 function

The GetREAL4 function takes two ushort parameters (first and second register). The function changes the parameters to bytes and then flipps the high and low end. Using BitConverter.ToSingle function I converted the two (flipped) parameters to one single float, which was then returned.

#### GetLONG function

The GetLONG function takes two ushort parameters (first and second register). The function changes the parameters to bytes and converts them to a hex string. The bytes two bytes are flipped and added together. Returns long.

#### GetError function

The GetError function takes one ushort parameter and checks which bit is high. If the there is a high bit it uses the right error message and returns a string. If no high bit then it returns a string "NoError".

#### IntegerToLowByte function

The GetError function takes one ushort parameter and picks the low byte which is returned as a integer.

#### Index.html

You can find the index.html in ModbusConvert\index.html. Added two lists and a progressbar for the signal quality. Used bootstrap for styling.

### Publishing

The code can be found in this repository and a hosted web app can be found at [Web app](http://benjaminhaldin-gmbtchallange.azurewebsites.net).


![High Five!](http://cdn1-www.craveonline.com/assets/uploads/2016/04/High-Five.jpg)