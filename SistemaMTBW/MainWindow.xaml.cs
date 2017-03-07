﻿using Gabriel.Cat.Extension;
using Microsoft.Win32;
using PokemonGBAFrameWork;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaMTBW
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RomGBA rom;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Desarrollado por Pikachu240 investigado por FBI y BLAx501! ","Sobre la App");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opnRom = new OpenFileDialog();
            opnRom.Filter = "Rom Pokemon GBA|*.gba";
            try
            {
                if (opnRom.ShowDialog().GetValueOrDefault())
                {
                    rom = new RomGBA(opnRom.FileName);
                    if (PokemonGBAFrameWork.SistemaMTBW.EstaActivadoElNuevoSistema(rom, Edicion.GetEdicion(rom), CompilacionRom.GetCompilacion(rom)))
                    {
                        PonTexto(true);
                    }
                    else PonTexto(false);
                    btnPonerOQuitar.IsEnabled = true;
                    switch(Edicion.GetEdicion(rom).AbreviacionRom)
                    {
                        case Edicion.ABREVIACIONESMERALDA:imgDecoración.SetImage(Imagenes.PokeballEsmeralda);break;
                        case Edicion.ABREVIACIONROJOFUEGO: imgDecoración.SetImage(Imagenes.PokeballRojoFuego); break;
                        case Edicion.ABREVIACIONVERDEHOJA: imgDecoración.SetImage(Imagenes.PokeballVerdeHoja); break;
                        case Edicion.ABREVIACIONRUBI: imgDecoración.SetImage(Imagenes.PokeballRuby); break;
                        case Edicion.ABREVIACIONZAFIRO: imgDecoración.SetImage(Imagenes.PokeballZafiro); break;
                    }
                }
                else if(rom!=null)
                {
                    MessageBox.Show("No se ha cambiado la rom","Atención",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }else
                {
                    MessageBox.Show("No se ha cargado nada...");
                }
            }catch
            {
                btnPonerOQuitar.IsEnabled = false;
                rom = null;
                imgDecoración.SetImage(new Bitmap(1, 1));
                MessageBox.Show("La rom no es compatible","Aun no es universal...");
            }
        }

        private void PonTexto(bool seTieneQueDesActivar)
        {
           if(seTieneQueDesActivar)
            {
                btnPonerOQuitar.Content = "Volver al sistema anterior";
            }
           else
            {
                btnPonerOQuitar.Content = "Poner Sistema MT BW!";
            }
        }

        private void btnPonerOQuitar_Click(object sender, RoutedEventArgs e)
        {
            if (PokemonGBAFrameWork.SistemaMTBW.EstaActivadoElNuevoSistema(rom, Edicion.GetEdicion(rom), CompilacionRom.GetCompilacion(rom)))
            {
                PokemonGBAFrameWork.SistemaMTBW.DesactivarNuevoSistema(rom, Edicion.GetEdicion(rom), CompilacionRom.GetCompilacion(rom));
                PonTexto(false);
            }
            else
            {
                PokemonGBAFrameWork.SistemaMTBW.ActivarNuevoSistema(rom, Edicion.GetEdicion(rom), CompilacionRom.GetCompilacion(rom));
                PonTexto(true);
            }
            rom.Guardar();
        }
    }
}
