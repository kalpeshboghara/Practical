using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Practical1.Models;
using System;
using System.Collections.Generic;

namespace Practical1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Palindrome

        public IActionResult CheckPalindrome()
        {
            return View(new PalindromeModel());
        }

        [HttpPost]
        public ActionResult CheckPalindrome(PalindromeModel model)
        {
            bool isPalindrome = true;
            if (model.InputNumber < 0)
            {
                ModelState.AddModelError("InputNumber", "Please enter positive Number");
            }
            else
            {
                string stringInput = model.InputNumber.ToString();
                for (int i = 0; i < stringInput.Length / 2; i++)
                {
                    if (stringInput[i] != stringInput[(^(i + 1))])
                    {
                        isPalindrome = false;
                    }
                }
                model = new PalindromeModel
                {
                    InputNumber = int.Parse(stringInput),
                    OutputString = isPalindrome ? string.Format("{0} is a palindrome", int.Parse(stringInput)) : string.Format("{0} is not a palindrome", int.Parse(stringInput)),
                    ShowResult = true
                };
            }

            return View(model);
        }

        #endregion

        #region Convert Number To String

        public IActionResult ConvertNumberToString()
        {
            return View(new ConvertNumberToStringModel());
        }

        [HttpPost]
        public ActionResult ConvertNumberToString(ConvertNumberToStringModel model)
        {
            Dictionary<int, string> threeString = new()
            {
                { 2, "hundred" },
                { 3, "thousand" }
            };

            Dictionary<int, string> oneString = new()
            {
                { 1, "one" },
                { 2, "two" },
                { 3, "three" },
                { 4, "four" },
                { 5, "five" },
                { 6, "six" },
                { 7, "seven" },
                { 8, "eight" },
                { 9, "nine" },
            };

            Dictionary<int, string> twoDString = new()
            {
                { 2, "twenty" },
                { 3, "thirty" },
                { 4, "forty" },
                { 5, "fifty" },
                { 6, "sixty" },
                { 7, "seventy" },
                { 8, "eighty" },
                { 9, "ninety" },
            };

            Dictionary<int, string> twoString = new()
            {
                { 10, "ten" },
                { 11, "eleven" },
                { 12, "twelve" },
                { 13, "thirteen" },
                { 14, "fourteen" },
                { 15, "fifteen" },
                { 16, "sixteen" },
                { 17, "seventeen" },
                { 18, "eighteen" },
                { 19, "nineteen" },
                { 20, "twenty" },
            };

            if (model.InputNumber >= 10000 || model.InputNumber == 0)
            {
                ModelState.AddModelError("InputNumber", "Please enter below 9999 , or Greater then zero");
            }
            else
            {
                var outputString = string.Empty;
                var stringInputs = model.InputNumber.ToString().Split('.', StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(stringInputs[0]))
                {
                    var string1 = stringInputs[0];
                    for (int i = 0; i < string1.Length; i++)
                    {
                        if (int.Parse(string1[i].ToString()) != 0)
                        {
                            switch (string1.Length - (i + 1))
                            {
                                case 3:
                                    outputString += $"{oneString[int.Parse(string1[i].ToString())]} {threeString[3]} ";
                                    break;
                                case 2:
                                    outputString += $"{oneString[int.Parse(string1[i].ToString())]} {threeString[2]} ";
                                    break;
                                case 1:
                                    if (int.Parse(string1[i].ToString()) == 1)
                                    {
                                        var stringNumber = $"{string1[i]}{string1[++i]}";
                                        outputString += $"{twoString[int.Parse(stringNumber.ToString())]} ";
                                    }
                                    else
                                    {
                                        if (int.Parse(string1[i + 1].ToString()) != 0)
                                        {
                                            outputString += $"{twoDString[int.Parse(string1[i].ToString())]}-{oneString[int.Parse(string1[++i].ToString())]}";
                                        }
                                        else
                                        {
                                            outputString += $"{twoDString[int.Parse(string1[i].ToString())]}";
                                        }
                                    }
                                    break;
                                case 0:
                                    outputString += $"{oneString[int.Parse(string1[i].ToString())]}";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(stringInputs[1]))
                {
                    var string1 = stringInputs[1];
                    if (!string.IsNullOrEmpty(outputString))
                    {
                        outputString += " and";
                    }
                    outputString += $" {string1}/{1.ToString().PadRight((string1.Length + 1), '0')}";
                }
                model = new ConvertNumberToStringModel
                {
                    OutputString = outputString,
                };
            }

            return View(model);
        }

        #endregion

        #region Print Number Spiral Format

        public IActionResult PrintNumberSpiralFormat()
        {
            return View(new PrintNumberSpiralFormatModel());
        }

        [HttpPost]
        public ActionResult PrintNumberSpiralFormat(PrintNumberSpiralFormatModel model)
        {
            if (model.InputNumber < 0)
            {
                ModelState.AddModelError("InputNumber", "Please enter Positive value");
            }
            else
            {
                var inputNumber = model.InputNumber;
                int[,] matrix = new int[model.InputNumber, model.InputNumber];
                int num = model.InputNumber * model.InputNumber;
                int row = 0, col = model.InputNumber - 1;

                for (int j = 0; j < (model.InputNumber / 2) + 1; j++)
                {
                    for (int i = col; i >= row; i--)
                    {
                        matrix[row, i] = --num;
                    }

                    for (int i = row + 1; i <= col; i++)
                    {
                        matrix[i, row] = --num;
                    }

                    for (int i = row + 1; i <= col; i++)
                    {
                        matrix[col, i] = --num;
                    }

                    for (int i = col - 1; i >= row + 1; i--)
                    {
                        matrix[i, col] = --num;
                    }
                    col--; row++;
                }


                model = new PrintNumberSpiralFormatModel
                {
                    InputNumber = inputNumber,
                    Matrix = matrix,
                };
            }

            return View(model);
        }

        #endregion
    }
}