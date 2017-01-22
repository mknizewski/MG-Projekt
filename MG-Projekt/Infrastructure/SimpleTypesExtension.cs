using MG_Projekt.BOL.Resources.Messages;
using System;
using System.Windows.Controls;

namespace MG_Projekt.Infrastructure
{
    public static class SimpleTypesExtension
    {
        public static int ToInt(this string text, Label label)
        {
            try
            {
                return int.Parse(text);
            }
            catch (Exception)
            {
                string labelName = label.Content.ToString();
                throw new Exception(MessageDictionary.PersonalizedExceptionMessage(labelName));
            }
        }

        public static double ToDouble(this string text, Label label)
        {
            try
            {
                string dotsToComma = text.Replace('.', ',');
                return double.Parse(dotsToComma);
            }
            catch (Exception)
            {
                string labelName = label.Content.ToString();
                throw new Exception(MessageDictionary.PersonalizedExceptionMessage(labelName));
            }
        }
    }
}