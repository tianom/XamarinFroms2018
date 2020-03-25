using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs e)
        {

            //validações
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {

                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {0},{1} {2} {3}", end.logradouro, end.bairro, end.localidade, end.uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "Endereço não encontrado para CEP informado "+ cep, "OK");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                }
            }

        }
        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if (cep.Length < 8 || cep.Length > 8)
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
                return false;
            }

            int NovoCEP = 0;

            if (!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve conter somente números.", "OK");
                //erro
                valido = false;
                return false;
            }

            return valido;
        }
    }
}
