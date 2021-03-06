﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanEjerySol.Engine
{
    class EngineSudoku
    {
        private EngineData Valor = EngineData.Instance();

        private int[] pos = new int[2];

        public TextBox[,] SetearTextColorInicio(TextBox[,] cajaTexto)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].BackColor = Color.WhiteSmoke;
                }
            }
            return cajaTexto;
        }                                

        public Button[] ColoresPincel(Button[] v)
        {
            v[0].BackColor = Color.Silver;
            v[1].BackColor = Color.SkyBlue;
            v[2].BackColor = Color.CornflowerBlue;
            v[3].BackColor = Color.LightCoral;
            v[4].BackColor = Color.Crimson;

            v[5].BackColor = Color.PaleGreen;
            v[6].BackColor = Color.YellowGreen;
            v[7].BackColor = Color.LightSalmon;
            v[8].BackColor = Color.Orange;
            return v;
        }

        public int[] Position(string sentido, int f, int c)
        {
            switch (sentido)
            {
                case "Up":
                    pos[0] = f - 1; pos[1] = c;
                    break;
                case "Down":
                    pos[0] = f + 1; pos[1] = c;
                    break;
                case "Right":
                    pos[0] = f; pos[1] = c + 1;
                    break;
                case "Left":
                    pos[0] = f; pos[1] = c - 1;
                    break;
            }
            return pos;
        }

        public  string  OrganizarCandidatos (string cadena , ListBox lista)
        {
            for (int i = 0; i <= cadena.Length - 1; i++) {
                if (cadena.Substring(i,1) =="1")
                {
                    if (!lista.Items.Contains("1"))
                        lista.Items.Add("1");
                }
                else if (cadena.Substring(i, 1) == "2")
                {
                    if (!lista.Items.Contains("2"))
                        lista.Items.Add("2");
                }
                else if (cadena.Substring(i, 1) == "3")
                {
                    if (!lista.Items.Contains("3"))
                        lista.Items.Add("3");
                }
                else if (cadena.Substring(i, 1) == "4")
                {
                    if (!lista.Items.Contains("4"))
                        lista.Items.Add("4");
                }
                else if (cadena.Substring(i, 1) == "5")
                {
                    if (!lista.Items.Contains("5"))
                        lista.Items.Add("5");
                }
                else if (cadena.Substring(i, 1) == "6")
                {
                    if (!lista.Items.Contains("6"))
                        lista.Items.Add("6");
                }
                else if (cadena.Substring(i, 1) == "7")
                {
                    if (!lista.Items.Contains("7"))
                        lista.Items.Add("7");
                }
                else if (cadena.Substring(i, 1) == "8")
                {
                    if (!lista.Items.Contains("8"))
                        lista.Items.Add("8");
                }
                else if (cadena.Substring(i, 1) == "9")
                {
                    if (!lista.Items.Contains("9"))
                        lista.Items.Add("9");
                }
            }

            string aux1 = string.Empty;
            string aux2 = string.Empty;

            for (int i = 0; i <= lista.Items.Count - 1; i++)
            {
                for (int j = 0; j <= lista.Items.Count - 1; j++)
                {
                    if (Convert.ToInt16(lista.Items[i]) <=  Convert.ToInt16(lista.Items[j]))
                    {
                        aux1 = lista.Items[i].ToString();
                        aux2 = lista.Items[j].ToString();
                        lista.Items[i] = aux2;
                        lista.Items[j] = aux1;
                    }
                }
            }

            string valor = string.Empty;
            int indic = 1;
            foreach (string v in lista.Items)
            {
                if (indic == 3 || indic == 6 || indic == 9)
                {
                    valor = valor + " " + v + Environment.NewLine;
                }
                else
                {
                    valor = valor + " " + v.Trim();
                }
                indic++;
            }

            return valor;
        }

        public bool ExisteValorIngresado(string[,] plantilla)
        {
            bool existeValor = false;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (plantilla[f, c] != null && plantilla[f, c] != string.Empty)
                    {
                        existeValor = true;
                        return existeValor;
                    }
                }
            }
            return existeValor;
        }

        public void ReadWriteTxt(string pathArchivo)
        {
            FileAttributes atributosAnteriores = File.GetAttributes(pathArchivo);
            File.SetAttributes(pathArchivo, atributosAnteriores & ~FileAttributes.ReadOnly);
        }

        public void OnlyReadTxt(string pathArchivo)
        {
            FileAttributes atributosAnteriores = File.GetAttributes(pathArchivo);
            File.SetAttributes(pathArchivo, atributosAnteriores | FileAttributes.ReadOnly);
        }

        public bool StatusOnlyReadTxt(string pathArchivo)
        {
            bool r = false;
            FileAttributes atributos = File.GetAttributes(pathArchivo);
            if ((atributos & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                r = true;
            }
            return r;
        }

        public bool ExiteArchivo(string pathArchivo)
        {
            bool resultado = false;
            if (File.Exists(pathArchivo))
            {
                resultado = true;
            }
            return resultado;
        }

        public void GuardarValoresIngresados(string pathArchivo, string[,] valorIngresado)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo))
                {
                    string vIngresado = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorIngresado[f, c] != null && valorIngresado[f, c] != string.Empty)
                            {
                                vIngresado = valorIngresado[f, c].Trim();
                            }
                            else
                            {
                                vIngresado = "0";
                            }
                            if (c == 0) vLinea = vIngresado + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vIngresado + "-";
                            else if (c == 8) vLinea = vLinea + vIngresado;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }
                }
            }
        }

        public void GuardarColoresIngresados(string pathArchivo, TextBox[,] cajaTexto)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo, true))
                {
                    string color = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (cajaTexto[f, c].BackColor == Color.SkyBlue)
                            {
                                color = "1";
                            }
                            else if (cajaTexto[f, c].BackColor == Color.CornflowerBlue)
                            {
                                color = "2";
                            }
                            else if (cajaTexto[f, c].BackColor == Color.LightCoral)
                            {
                                color = "3";
                            }
                            else if (cajaTexto[f, c].BackColor == Color.Crimson)
                            {
                                color = "4";
                            }
                            else if (cajaTexto[f, c].BackColor == Color.PaleGreen)
                            {
                                color = "5";
                            }
                            else if (cajaTexto[f, c].BackColor == Color.YellowGreen)
                            {
                                color = "6";
                            }
                            else if (cajaTexto[f, c].BackColor == Color.LightSalmon)
                            {
                                color = "7";
                            }
                            else if (cajaTexto[f, c].BackColor == Color.Orange)
                            {
                                color = "8";
                            }
                            else
                            {
                                color = "0";
                            }
                            if (c == 0) vLinea = color + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + color + "-";
                            else if (c == 8) vLinea = vLinea + color;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }

                }
            }
        }

        public TextBox[,] SetearTextBoxLimpio(TextBox[,] cajaTexto)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].Text = string.Empty;
                }
            }
            return cajaTexto;
        }

    }
}
