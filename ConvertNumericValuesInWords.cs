using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConvertNumericValuesInWords
/// </summary>
public class ConvertNumericValuesInWords
{
    public ConvertNumericValuesInWords()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public String Ones(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = "";
        switch (_Number)
        {

            case 1:
                name = "един";
                break;
            case 2:
                name = "два";
                break;
            case 3:
                name = "три";
                break;
            case 4:
                name = "четири";
                break;
            case 5:
                name = "пет";
                break;
            case 6:
                name = "шест";
                break;
            case 7:
                name = "седем";
                break;
            case 8:
                name = "осем";
                break;
            case 9:
                name = "девет";
                break;
        }

        return name;
    }

    public String Tens(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = null;
        switch (_Number)
        {
            case 10:
                name = "десет";
                break;
            case 11:
                name = "единадесет";
                break;
            case 12:
                name = "дванадесет";
                break;
            case 13:
                name = "тринадесет";
                break;
            case 14:
                name = "четиринадесет";
                break;
            case 15:
                name = "петнадесет";
                break;
            case 16:
                name = "шестнадесет";
                break;
            case 17:
                name = "седемнадесет";
                break;
            case 18:
                name = "осемнадесет";
                break;
            case 19:
                name = "деветнадесет";
                break;
            case 20:
                name = "двадесет";
                break;
            case 30:
                name = "тридесет";
                break;
            case 40:
                name = "четиридесет";
                break;
            case 50:
                name = "петдесет";
                break;
            case 60:
                name = "шестдесет";
                break;
            case 70:
                name = "седемдесет";
                break;
            case 80:
                name = "осемдесет";
                break;
            case 90:
                name = "деветдесет";
                break;
            default:
                if (_Number > 0)
                {
                    name = Tens(Number.Substring(0, 1) + "0") + " и " + Ones(Number.Substring(1));
                }
                break;
        }
        return name;
    }

    public String ConvertWholeNumber(String Number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX    
            bool isDone = false;//test if already translated    
            double dblAmt = (Convert.ToDouble(Number));
            //if ((dblAmt > 0) && number.StartsWith("0"))    
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric    
                beginsZero = Number.StartsWith("0");

                int numDigits = Number.Length;
                int pos = 0;//store digit grouping    
                String place = "";//digit grouping name:hundres,thousand,etc...    
                switch (numDigits)
                {
                    case 1://ones' range    

                        word = Ones(Number);
                        isDone = true;
                        break;
                    case 2://tens' range    
                        word = Tens(Number);
                        isDone = true;
                        break;
                    case 3://hundreds' range    
                        pos = (numDigits % 3) + 1;
                        place = "стотин ";
                        break;
                    case 4://thousands' range    
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " хиляди ";
                        break;
                    case 7://millions' range    
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " милиона ";
                        break;
                    case 10://Billions's range    
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " милиарда ";
                        break;
                    //add extra case options for anything above Billion...    
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)    
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                        }
                        catch { }
                    }
                    else
                    {
                        word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                    }

                    //check for trailing zeros    
                    //if (beginsZero) word = " and " + word.Trim();    
                }
                //ignore digit grouping names    
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch { }

        if (word.Contains("единстотин"))
        {
            word = word.Replace("единстотин", "сто");
        }
        if (word.Contains("двастотин"))
        {
            word = word.Replace("двастотин", "двеста");
        }
        if (word.Contains("тристотин"))
        {
            word = word.Replace("тристотин", "триста");
        }

        if (word.Contains("един хиляди"))
        {
            word = word.Replace("един хиляди", "хиляда");

        }
        if (word.Contains("два хиляди"))
        {
            word = word.Replace("два хиляди", "две хиляди");
        }

        if (word.Contains("един милиона"))
        {
            word = word.Replace("милиона", "милион");
        }
        if (word.Contains("милиона хиляди") || word.Contains("милион хиляди"))
        {
            word = word.Replace("хиляди", " ");
        }

        if (word.Contains("един милиарда"))
        {
            word = word.Replace("милиарда", "милиард");
        }

        if (word.Contains("милиарда милион") || word.Contains("милиард милион"))
        {
            word = word.Replace("милион", " ");
        }

        return word.Trim();
    }


    public String SaySum(String numb)
    {
        String val = "", wholeNo = numb, points = "", andStr = "", stotinkiStr = "", result = "";

        String endStr = "лeва";
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = " и";// just to separate whole numbers from points/cents    
                    stotinkiStr = " ст.";//Cents    
                    //pointStr = ConvertDecimals(points);
                }
                if (points == "00")
                {
                    points = "";
                }
            }

            var numberInWords = ConvertWholeNumber(wholeNo).Trim();

            val = String.Format("{0} {1}{2} {3}{4}", numberInWords, endStr, andStr, points, stotinkiStr);


            if (val.Contains("десет лeва") || val.Contains("единадесет лeва") || val.Contains("дванадесет лeва") || val.Contains("тринадесет лeва") ||
            val.Contains("четиринадесет лeва") || val.Contains("петнадесет лeва") || val.Contains("шестнадесет лeва") || val.Contains("седемнадесет лeва") ||
            val.Contains("осемнадесет лeва") || val.Contains("деветнадесет лeва") || val.Contains("двадесет лeва") ||
            val.Contains("тридесет лeва") || val.Contains("четиридесет лeва") || val.Contains("петдесет лeва") || val.Contains("шестдесет лeва") ||
            val.Contains("седемдесет лeва") || val.Contains("осемдесет лeва") || val.Contains("деветдесет лeва") || val.Contains("сто лeва") || val.Contains("двеста лeва")
            || val.Contains("триста лeва") || val.Contains("четиристотин лeва") || val.Contains("петстотин лeва") || val.Contains("шестстотин лeва") || val.Contains("седемстотин лeва")
            || val.Contains("осемстотин лeва") || val.Contains("деветстотин лeва"))
            {

                var splited = val.Trim().Split(new[] { "лeва" }, StringSplitOptions.None).First().Trim();
                var firstPart = splited.Split(' ').ToList();
                var index = firstPart.Count - 1;

                if (firstPart.Count > 1)
                {
                    for (int i = 0; i < firstPart.Count; i++)
                    {

                        if (i == index)
                        {
                            result += " и " + firstPart[i];
                        }
                        else
                        {
                            result += firstPart[i] + " ";
                        }

                    }

                    result = String.Format("{0} {1}{2} {3}{4}", result, endStr, andStr, points, stotinkiStr);
                }
                else
                {
                    result = String.Format("{0} {1}{2} {3}{4}", numberInWords, endStr, andStr, points, stotinkiStr);
                }

            }
            else
            {
                result = String.Format("{0} {1}{2} {3}{4}", numberInWords, endStr, andStr, points, stotinkiStr);
            }

        }
        catch { }
        return result;
    }

    public String ConvertDecimals(String number)
    {
        String cd = "", digit = "", engOne = "";
        for (int i = 0; i < number.Length; i++)
        {
            digit = number[i].ToString();
            if (digit.Equals("0"))
            {
                engOne = "Нула";
            }
            else
            {
                engOne = Ones(digit);
            }
            cd += " " + engOne;
        }
        return cd;
    }



    //English version
    public String OnesEng(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = "";
        switch (_Number)
        {

            case 1:
                name = "one";
                break;
            case 2:
                name = "two";
                break;
            case 3:
                name = "three";
                break;
            case 4:
                name = "four";
                break;
            case 5:
                name = "five";
                break;
            case 6:
                name = "six";
                break;
            case 7:
                name = "seven";
                break;
            case 8:
                name = "eight";
                break;
            case 9:
                name = "nine";
                break;
        }
        return name;
    }

    public String TensEng(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = null;
        switch (_Number)
        {
            case 10:
                name = "ten";
                break;
            case 11:
                name = "eleven";
                break;
            case 12:
                name = "twelve";
                break;
            case 13:
                name = "thirteen";
                break;
            case 14:
                name = "fourteen";
                break;
            case 15:
                name = "fifteen";
                break;
            case 16:
                name = "sixteen";
                break;
            case 17:
                name = "seventeen";
                break;
            case 18:
                name = "eighteen";
                break;
            case 19:
                name = "nineteen";
                break;
            case 20:
                name = "twenty";
                break;
            case 30:
                name = "thirty";
                break;
            case 40:
                name = "fourty";
                break;
            case 50:
                name = "fifty";
                break;
            case 60:
                name = "sixty";
                break;
            case 70:
                name = "seventy";
                break;
            case 80:
                name = "eighty";
                break;
            case 90:
                name = "ninety";
                break;
            default:
                if (_Number > 0)
                {
                    name = TensEng(Number.Substring(0, 1) + "0") + " " + OnesEng(Number.Substring(1));
                }
                break;
        }
        return name;
    }

    public String ConvertWholeNumberEng(String Number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX    
            bool isDone = false;//test if already translated    
            double dblAmt = (Convert.ToDouble(Number));
            //if ((dblAmt > 0) && number.StartsWith("0"))    
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric    
                beginsZero = Number.StartsWith("0");

                int numDigits = Number.Length;
                int pos = 0;//store digit grouping    
                String place = "";//digit grouping name:hundres,thousand,etc...    
                switch (numDigits)
                {
                    case 1://ones' range    

                        word = OnesEng(Number);
                        isDone = true;
                        break;
                    case 2://tens' range    
                        word = TensEng(Number);
                        isDone = true;
                        break;
                    case 3://hundreds' range    
                        pos = (numDigits % 3) + 1;
                        place = " hundred ";
                        break;
                    case 4://thousands' range    
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " thousand ";
                        break;
                    case 7://millions' range    
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " million ";
                        break;
                    case 10://Billions's range    
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " billion ";
                        break;
                    //add extra case options for anything above Billion...    
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)    
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = ConvertWholeNumberEng(Number.Substring(0, pos)) + place + ConvertWholeNumberEng(Number.Substring(pos));
                        }
                        catch { }
                    }
                    else
                    {
                        word = ConvertWholeNumberEng(Number.Substring(0, pos)) + ConvertWholeNumberEng(Number.Substring(pos));
                    }

                    //check for trailing zeros    
                    //if (beginsZero) word = " and " + word.Trim();    
                }
                //ignore digit grouping names    
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch { }

        if (word.Contains("million thousand"))
        {
            word = word.Replace("thousand", " ");
        }
        if (word.Contains("billion million"))
        {
            word = word.Replace("million", " ");
        }

        return word.Trim();
    }

    public String SaySumEng(String numb)
    {
        String val = "", wholeNo = numb, points = "", andStr = "", stotinkiStr = "";
        String endStr = "leva";
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = "and";// just to separate whole numbers from points/cents    
                    stotinkiStr = "st.";//Cents    
                    //pointStr = ConvertDecimalsEng(points);
                }
            }
            val = String.Format("{0} {1} {2} {3} {4}", ConvertWholeNumberEng(wholeNo).Trim(), endStr, andStr, points, stotinkiStr);
        }
        catch { }
        return val;
    }

    public String ConvertDecimalsEng(String number)
    {
        String cd = "", digit = "", engOne = "";
        for (int i = 0; i < number.Length; i++)
        {
            digit = number[i].ToString();
            if (digit.Equals("0"))
            {
                engOne = "Zero";
            }
            else
            {
                engOne = OnesEng(digit);
            }
            cd += " " + engOne;
        }
        return cd;
    }



}