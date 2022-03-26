using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraSimple {
    public partial class CalculatorForm : Form {
        /*Region para las variables que se van a utilizar en el programa*/
        #region variables
        string[] _operatorList = new string[] { "+", "-", "x", "/", "r" };
        double? _reservedNumber1 = null, _reservedNumber2 = null;
        string? _operator = null;
        bool _clearText = false;
        #endregion

        public CalculatorForm() {
            InitializeComponent();
        }

        /*Este evento esta agregado a todos los botones de la calculadora*/
        private void btn0_Click(object sender, EventArgs e) {
            var text = ((Button)sender).Text;

            if (text == "C") { // Si el boton es C
                txtResult.Clear();
            } else if (text == "del") { // Si el boton es del
                string txt = txtResult.Text;
                string newTxt = "";
                for (int i = 0; i < txt.Length - 1; i++) newTxt += txt[i];
                txtResult.Text = newTxt;
            } else { // Si el boton es un numero u operador
                var isOperator = _operatorList.Any(o => o == text);

                if (isOperator) { // si es operador
                    _clearText = true;

                    if (_operator != null) { // si ya estamos haciendo una operacion previa
                        if (double.TryParse(txtResult.Text, out double aux)) {
                            _reservedNumber2 = aux;
                        }

                        calculate();
                    }

                    if (double.TryParse(txtResult.Text, out double temp)) {
                        _reservedNumber1 = temp;
                        _operator = text;

                        if (_operator == "r") {
                            if (double.TryParse(txtResult.Text, out double aux)) {
                                _operator = null;
                                double sqrt = Math.Sqrt(aux);
                                _reservedNumber1 = sqrt;
                                txtResult.Text = "" + sqrt;
                            }
                        }
                    }
                } else if (text == "=") { // si el boton es igual
                    if(_reservedNumber1 != null && _operator != null) {
                        if (double.TryParse(txtResult.Text, out double temp)) {
                            _reservedNumber2 = temp;
                        }

                        calculate();
                        _clearText = true;
                    }

                    _operator = null;
                } else { // si el boton es un . o un numero
                    if (_clearText) {
                        txtResult.Text = text;
                        _clearText = false;
                    } else {
                        if (txtResult.Text == "0") {
                            txtResult.Text = text;
                        } else {
                            txtResult.Text += text;
                        }
                    }
                }
            }
        }

        /*Metodo para calcular el resultado de las operaciones*/
        private void calculate() {
            double? result = 0;

            switch (_operator) {
                case "+": result = _reservedNumber1 + _reservedNumber2; break;
                case "-": result = _reservedNumber1 - _reservedNumber2; break;
                case "x": result = _reservedNumber1 * _reservedNumber2; break;
                case "/": result = _reservedNumber1 / _reservedNumber2; break;
            }

            txtResult.Text = result.ToString();
        }
    }
}
