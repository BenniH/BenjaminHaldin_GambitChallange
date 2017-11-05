using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Benjaminhaldin_GambitChallange.Models;
using System.Net;
using System.IO;

namespace Benjaminhaldin_GambitChallange.Controllers
{
    public class ModbusConvertController : Controller
    {
        public IActionResult Index()
        {
            string url = "http://tuftuf.gambitlabs.fi/feed.txt";
            WebClient client = new WebClient();
            using (var stream = client.OpenRead(url))
            using (var reader = new StreamReader(stream))
            {
                string line;
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string register = line.Substring(0, line.IndexOf(":"));
                    if (counter == 0)
                    {
                        ViewData["ReadDate"] = line;
                        counter++;
                    }
                    string reg1;
                    string reg2;
                    switch (register)
                    {
                        case "1": // REAL4 Unit m3/h
                            reg1 = line.Substring(line.IndexOf(":")+1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":")+1);
                            ViewData["FlowRate"] = "Flow Rate: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2))) + " m3/h";
                            break;
                        case "3": // REAL4 unit GJ/h
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["EFlowRate"] = "Energy Flow Rate: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2))) + " GJ/h";
                            break;
                        case "5": // REAL4 unit m/s
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["Velocity"] = "Velocity: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2))) + " m/s";
                            break;
                        case "7": // REAL4 unit m/s
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["FluidSound"] = "Fluid Sound Speed: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2))) + " m/s";
                            break;
                        case "9": // LONG
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["PosAccumulator"] = "Positive accumulator: " + Convert.ToString(GetLONG(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "11": // REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["PosDecFraction"] = "Positive decimal fraction: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "13": // LONG
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["NegAccumulator"] = "Negative accumulator: " + Convert.ToString(GetLONG(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "15": // REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["NegDecFraction"] = "Negative decimal fraction: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "17": // LONG
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["PosEnrgAcc"] = "Positive energy accumulator: " + Convert.ToString(GetLONG(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "19": // REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["PosEnrgDecFraction"] = "Positive energy decimal fraction: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "21": // LONG
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["NegEnrgAcc"] = "Negative energy accumulator: " + Convert.ToString(GetLONG(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "23": // REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["NegEnrgDecFraction"] = "Negative energy decimal fraction: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "25": // LONG
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["NetAcc"] = "Net accumulator: " + Convert.ToString(GetLONG(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "27": // REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["NetDecFraction"] = "Net decimal fraction: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "29": // LONG
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["NetEnrgAcc"] = "Net energy accumulator: " + Convert.ToString(GetLONG(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "31": // REAL4 
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["NetEnrgDecFraction"] = "Net energy decimal fraction: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "33": // REAL4 unit Celcius
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["Temperature1"] = "Temperature #1(inlet): " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " C");
                            break;
                        case "35": // REAL4 unit Celcius
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["Temperature2"] = "Temperature #2(outlet): " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " C");
                            break;
                        case "37": // REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["AnalogInAI3"] = "Analog input AI3: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "39": // REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["AnalogInAI4"] = "Analog input AI4: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "41": // REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["AnalogInAI5"] = "Analog input AI5: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "43": // REAL4 mA
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["CurrentInAI3-1"] = "Current input at AI3: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " mA");
                            break;
                        case "45": // REAL4 mA
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["CurrentInAI3-2"] = "Current input at AI3: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " mA");
                            break;
                        case "47": // REAL4 mA
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["CurrentInAI3-3"] = "Current input at AI3: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " mA");
                            break;
                        case "49": // ignore
                            break;
                        case "51": // ignore
                            break;
                        case "53": // ignore
                            break;
                        case "56": // ignore 
                            break;
                        case "59": // single int Dihä behövs säkert int visas?
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            string intoVoid = "Key To Input: " + Convert.ToString(reg1);
                            break;
                        case "60": // single Int Dihä behövs säkert int visas?
                            break;
                        case "61": // single Int unit/sec Dihä behövs säkert int visas?
                            break;
                        case "62": // single Int Dihä behövs säkert int visas?
                            break;
                        case "72": // single 16bits int note4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            ViewData["ErrorMessage"] = GetError(Convert.ToUInt16(reg1));
                            break;
                        case "77": // Ohm REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["ResistanceInlet"] = "PT100 resistance of inlet: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " \u2126");
                            break;
                        case "79": // Ohm REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["ResistanceOutlet"] = "PT100 resistance of outlet: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " \u2126");
                            break;
                        case "81": // Micro-second REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["TotalTravel"] = "Total travel time: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " \u00B5" + "s");
                            break;
                        case "83": // Nino-second REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["TotalDeltaTravel"] = "Delta travel time: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " Nino-second"); // nano?
                            break;
                        case "85": // Micro-second REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["UpStreamTravel"] = "Up stream travel time: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " \u00B5" + "s");
                            break;
                        case "87": // Micro-second REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["DownStreamTravel"] = "Downstream travel time: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " \u00B5" + "s");
                            break;
                        case "89": // unit mA REAL4
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["OutputCurrent"] = "Output current: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)) + " mA");
                            break;
                        case "92": // single High Low byte int
                            reg1 = line.Substring(line.IndexOf(":")+1);
                            ViewData["SignalQuality"] = Convert.ToString(IntegerToLowByte(Convert.ToUInt16(reg1)));
                            break;
                        case "93": // single range 0-2047
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            if(Convert.ToInt16(reg1) > 2047)
                            {
                                reg1 = "2047";
                            }
                            ViewData["UpStreamStr"] = "Upstream strength: " + reg1;
                            break;
                        case "94": // single range 0-2047
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            if (Convert.ToInt16(reg1) > 2047)
                            {
                                reg1 = "2047";
                            }
                            ViewData["DownStreamStr"] = "Downstream strength: " + reg1;
                            break;
                        case "96": // single 1 or 0
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            string lang;
                            if (reg1 == "1")
                                lang = "Chinese";
                            else
                                lang = "English";                            
                            ViewData["Language"] = "Language: " + lang;
                            break;
                        case "97": // REAL4 Normal 100 +-3%
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["MeasuredTravelToCalcTravel"] = "The rate of the measured travel time by the calculated travel time (Normal 100 +-3%): " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;
                        case "99": // REAL4 
                            reg1 = line.Substring(line.IndexOf(":") + 1);
                            reg2 = reader.ReadLine();
                            reg2 = reg2.Substring(reg2.IndexOf(":") + 1);
                            ViewData["Raynolds"] = "Raynolds number: " + Convert.ToString(GetREAL4(Convert.ToUInt16(reg1), Convert.ToUInt16(reg2)));
                            break;

                    }                    
                }
            }              
            return View();

        }
        public float GetREAL4(ushort highOrderValue, ushort lowOrderValue) // two integers flipped and turned into Float
        {
            byte[] high = BitConverter.GetBytes(highOrderValue);
            byte[] low = BitConverter.GetBytes(lowOrderValue);

            // AB CD --> CD AB (flip)
            lowOrderValue = BitConverter.ToUInt16(high, 0);
            highOrderValue = BitConverter.ToUInt16(low, 0);

            float lol = BitConverter.ToSingle(BitConverter.GetBytes(lowOrderValue).Concat(BitConverter.GetBytes(highOrderValue)).ToArray(), 0);
            return lol;
        }
        public long GetLONG(ushort highOrderValue, ushort lowOrderValue) // two integers flipped and turned into Long
        {
            byte[] high = BitConverter.GetBytes(highOrderValue);
            byte[] low = BitConverter.GetBytes(lowOrderValue);
            // AB
            string hexValue = highOrderValue.ToString("X");
            // CD
            string hexValue2 = lowOrderValue.ToString("X");
            string hexA = "0";
            string hexB = "0";
            string hexC = "0";
            string hexD = "0";
            if (hexValue.Length >= 2)
            {
                hexA = hexValue.Substring(0, 2);
                hexB = hexValue.Substring(2);
            }
            else if(hexValue.Length == 1)
            {
                hexA = hexValue.Substring(0, 1);
                hexB = "0";
            }
            //hexB = hexValue.Substring(2);

            if (hexValue2.Length >= 2)
            {
                hexC = hexValue2.Substring(0, 2);
                hexD = hexValue2.Substring(2);
            }
            else if (hexValue2.Length == 1)
            {
                hexC = hexValue2.Substring(0, 1);
                hexD = "0";
            }
            //hexD = hexValue.Substring(2);

            // CD AB (flip)
            string hex = hexC + hexD + hexA + hexB;

            // omvandla tibaks till int
            long intAgain = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);

            return intAgain;
        }
        public int IntegerToLowByte(ushort integer) // taking low byte from 1 integer
        {
            byte[] integerInByte = BitConverter.GetBytes(integer);
            int lowByte = integerInByte[0];

            return lowByte;
        }
        public string GetError(ushort integer) // taking low byte from 1 integer
        {
            byte[] bytes = BitConverter.GetBytes(integer);
            // 0000 0000 0000 0000
            byte b = bytes[0];
            int error = -1;
            string text = "";
            for(int i = 0; i <= 15; i++)
            { 
                if((b & (1 << i)) != 0)
                {
                    error = (b & (1 << i));
                }
            }
            switch (error)
            {
                case -1:
                    text = "NoError";
                    break;
                case 0:
                    text = "No signal";
                    break;
                case 1:
                    text = "Low recieved signal";
                    break;
                case 2:
                    text = "Poor recieved signal";
                    break;
                case 3:
                    text = "Pipe empty";
                    break;
                case 4:
                    text = "Hardware failure";
                    break;
                case 5:
                    text = "Receiving circuits gain in adjusting";
                    break;
                case 6:
                    text = "Frequency at the frequency output over flow";
                    break;
                case 7:
                    text = "Current at 4-20mA over flow";
                    break;
                case 8:
                    text = "RAM check-sum error";
                    break;
                case 9:
                    text = "Main clock or timer clock error";
                    break;
                case 10:
                    text = "Parameters check-sum error";
                    break;
                case 11:
                    text = "ROM check-sum error";
                    break;
                case 12:
                    text = "Temperature circuits error";
                    break;
                case 13:
                    text = "Reserved";
                    break;
                case 14:
                    text = "Internal timer over flow";
                    break;
                case 15:
                    text = "Analog input over range";
                    break;
            }

            return text;
        }

    }
}