using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.DataAccess
{
    public  static class UtilsDataAcces
    {
        /// <summary>
        /// Metoto encargado de validar los valores 
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="replaceValue"></param>
        /// <param name="isPermitateVelueZero"></param>
        /// <returns></returns>
        public  static String ValidateDiferentString(String currentValue, String replaceValue, Boolean isPErmitateValueZero, Boolean currentUpdatedValue, out Boolean updatedValue)
        {
            updatedValue = currentUpdatedValue;
            if (!String.IsNullOrEmpty(replaceValue))
            {

                if (!isPErmitateValueZero && replaceValue.Trim().Equals("0"))
                    return currentValue;

                else if ((String.IsNullOrEmpty(currentValue) && !String.IsNullOrEmpty(replaceValue) && currentValue != replaceValue) || (!currentValue.Trim().ToUpper().Equals(replaceValue.Trim().ToUpper()) && !String.IsNullOrEmpty(replaceValue)))
                {
                    updatedValue = true;
                    return replaceValue;
                }
            }
            return currentValue;
        }


        /// <summary>
        /// Meotto encargado de comaprar dos valores entereros y 
        /// devolver el valor a remplazar y por referencia retorna si 
        /// han ocurrido cmabios o noi 
        /// conservando el valor de la variable que ha ingreso por referencia
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="replaceValue"></param>
        /// <param name="isPErmitateValueZero"></param>
        /// <param name="currentUpdatedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns></returns>
        public  static int? ValidateDiferentInt(int? currentValue, int? replaceValue, Boolean isPErmitateValueZero, Boolean currentUpdatedValue, out Boolean updatedValue)
        {
            updatedValue = currentUpdatedValue;
            String value = ValidateDiferentString(currentValue?.ToString(), replaceValue?.ToString(), isPErmitateValueZero, currentUpdatedValue, out updatedValue);
            if (!String.IsNullOrEmpty(value) && int.TryParse(value, out int replace))
                return replace;

            return currentValue;
        }


        /// <summary>
        /// Encargado de convertir un doble de la base de datos 
        /// y retornar si ocurrieron cambios por referencia 
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="replaceValue"></param>
        /// <param name="isPErmitateValueZero"></param>
        /// <param name="currentUpdatedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns></returns>
        public  static Double? ValidateDiferentDouble(Double? currentValue, Double? replaceValue, Boolean isPErmitateValueZero, Boolean currentUpdatedValue, out Boolean updatedValue)
        {
            updatedValue = currentUpdatedValue;
            String value = ValidateDiferentString(currentValue?.ToString(), replaceValue?.ToString(), isPErmitateValueZero, currentUpdatedValue, out updatedValue);
            if (Double.TryParse(value, out Double replace))
                return replace;
            return currentValue;
        }


        /// <summary>
        /// Valida Si un decial tiene diferencias y retorna
        /// el valor nuevo si ha cambiado
        /// y por referecnia si ha cambiado correctamebnte o no
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="replaceValue"></param>
        /// <param name="isPErmitateValueZero"></param>
        /// <param name="currentUpdatedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns></returns>
        public  static Decimal? ValidateDiferentDecimal(Decimal? currentValue, Decimal? replaceValue, Boolean isPErmitateValueZero, Boolean currentUpdatedValue, out Boolean updatedValue)
        {
            updatedValue = currentUpdatedValue;
            String value = ValidateDiferentString(currentValue?.ToString(), replaceValue?.ToString(), isPErmitateValueZero, currentUpdatedValue, out updatedValue);
            if (Decimal.TryParse(value, out Decimal replace))
                return replace;
            return currentValue;
        }

        /// <summary>
        /// Valdia si un short tiene diferencia y retrona el valor nuevo si ha cambiado 
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="replaceValue"></param>
        /// <param name="isPErmitateValueZero"></param>
        /// <param name="currentUpdatedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns></returns>
        public  static short? ValidateDiferentShort(short? currentValue, short? replaceValue, bool isPErmitateValueZero, bool currentUpdatedValue, out bool updatedValue)
        {
            updatedValue = currentUpdatedValue;
            String value = ValidateDiferentString(currentValue?.ToString(), replaceValue?.ToString(), isPErmitateValueZero, currentUpdatedValue, out updatedValue);
            if (short.TryParse(value, out short replace))
                return replace;

            return currentValue;
        }

        /// <summary>
        /// Metotoe encargado de comparar dos date y de volver el 
        /// dato por el que se va a remplazar y por referencia devolver si el dato se ha remplazado correctamente
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="replaceValue"></param>
        /// <param name="isPErmitateValueZero"></param>
        /// <param name="currentUpdatedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns></returns>
        public  static DateTime? ValidateDiferentDateTime(DateTime? currentValue, DateTime? replaceValue, Boolean currentUpdatedValue, out Boolean updatedValue)
        {
            updatedValue = currentUpdatedValue;
            String value = ValidateDiferentString(currentValue?.ToString(), replaceValue?.ToString(), false, currentUpdatedValue, out updatedValue);
            if (DateTime.TryParse(value, out DateTime replace))
                return replace;
            return currentValue;
        }

        /// <summary>
        /// Metoto encargado de validar diferencias entres dos booleanos
        /// y devovler el valor diferente
        /// por referencia retorna si el valor fue cambiado
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="replaceValue"></param>
        /// <param name="currentUpdatedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns></returns>
        public  static Boolean? ValidateDiferentBoolean(Boolean? currentValue, Boolean? replaceValue, Boolean currentUpdatedValue, out Boolean updatedValue)
        {
            updatedValue = currentUpdatedValue;
            if (currentValue != replaceValue && replaceValue != null)
            {
                updatedValue = true;
                return replaceValue;
            }
            return currentValue;
        }


        /// <summary>
        ///Valida si un long ha cambiado si es asi lo actualiza
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="replaceValue"></param>
        /// <param name="isPErmitateValueZero"></param>
        /// <param name="currentUpdatedValue"></param>
        /// <param name="updatedValue"></param>
        /// <returns></returns>
        public  static long? ValidateDiferentLong(long? currentValue, long? replaceValue, bool isPErmitateValueZero, bool currentUpdatedValue, out bool updatedValue)
        {
            updatedValue = currentUpdatedValue;
            String value = ValidateDiferentString(currentValue?.ToString(), replaceValue?.ToString(), isPErmitateValueZero, currentUpdatedValue, out updatedValue);
            if (long.TryParse(value, out long replace))
                return replace;

            return currentValue;
        }
    }
}
