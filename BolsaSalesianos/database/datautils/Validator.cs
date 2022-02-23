using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace BolsaSalesianos.database
{
    internal class Validator
    {
        public Snackbar trigger { get; set; }
        public Validator(Snackbar trigger)
        {
            this.trigger = trigger;
        }

        /* 
         * El método validate recibe un tipo de valor y su valor correspondiente, reocorre un switch con todos mis posibles
         * tipos de datos y devuelve true o false comprobando los valores.
         */
        private bool Validate(string value_name, string value)
        {
            switch (value_name)
            {
                case "name": return ValidateWithError(value, @"^((\w|ñ){2,} *){1,4}$", "El nombre es incorrecto");
                case "last_name": return ValidateWithError(value, @"^((\w|ñ){2,} *){1,4}$", "Los apellidos son incorrectos");
                case "contact_person": return ValidateWithError(value, @"^((\w|ñ){2,} *){1,4}$", "El nombre es incorrecto");
                case "dni": return ValidateWithError(value, @"^\d{8}[a-zA-Z]$", "El DNI es incorrecto");
                case "cif": return ValidateWithError(value, @"^\d{9}$", "El CIF es incorrecto");
                case "user": return ValidateWithError(value, @"^(\w|ñ){2,}$", "El usuario es incorrecto");
                case "pass_2": return Regex.IsMatch(value, @"^(?=.*([A-Z]|Ñ))(?=.*([a-z]|ñ))(?=.*[0-9])(?=.*[#?!@$ %^&*-]).{6,}$");
                case "pass": return ValidateWithError(value, @"^(?=.*([A-Z]|Ñ))(?=.*([a-z]|ñ))(?=.*[0-9])(?=.*[#?!@$ %^&*-]).{6,}$", "La contraseña es incorrecta");
                case "email": return ValidateWithError(value, @"^\w+@\w+[.]\w{2,3}$", "El email es incorrecto");
                case "phone": return ValidateWithError(value, @"^([+](\d{1,3}([-]\d{1,3})?) ?)?\d{9}$", "El telefono es incorrecto");
                default: return true;
            }
        }

        /* 
         * Este método valida un dato he introduce un error a la cola de mensajes
         * de error.
         */
        private bool ValidateWithError(string value, string regex, string error)
        {
            if (!Regex.IsMatch(value, regex))
            {
                trigger.MessageQueue.Enqueue(error);
                return false;
            }
            return true;
        }

        /* 
         * Recibe una lista lista de valores, crea una lista de mensajes de error y recorre
         * todos los valores recibidos pasando la clave y el valor a tráves del metodo validate,
         * que este retornará true o false dependiendo si el valor corresponde a una expresión
         * regular específica.
         */
        public bool IsValid(Dictionary<string, string> values)
        {
            bool isValid = true;
            trigger.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            foreach (var value in values)
                if (!Validate(value.Key, value.Value))
                    isValid = false;

            return isValid;
        }

        /* 
         * Este método recibe un stackpanel el cuál debería de recibir un panel que unícamente contenga los elementos
         * que correspondan a un formulario, en caso de que los elementos no sean de los tipos establecidos no se va-
         * loraran, el método recogerá todos los datos del formulario y los almacenará en un diccionario.
         */
        public Dictionary<String, String> getValues(StackPanel stackPanel, string form_name)
        {
            Dictionary<String, String> values = new Dictionary<String, String>();
            foreach (object child in stackPanel.Children)
            {
                switch (child)
                {
                    case TextBox textBox:
                        values.Add(textBox.Name.Replace($"{form_name}_", ""), textBox.Text);
                        break;
                    case PasswordBox passwordBox:
                        values.Add(passwordBox.Name.Replace($"{form_name}_", ""), passwordBox.Password);
                        break;
                }
            }
            return values;
        }

    }
}
