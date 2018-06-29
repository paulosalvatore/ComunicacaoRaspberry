using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;
using System;

public class Controlador : MonoBehaviour
{
	public SerialPort raspberry;
	public Dropdown listaPortasDropdown;
	public InputField consoleText;

	private string[] nomesPortas;
	private Dictionary<string, SerialPort> portas = new Dictionary<string, SerialPort>();

	private void Start()
	{
		AtualizarListaPortasDropdown();
	}

	public void AtualizarListaPortasDropdown()
	{
		List<Dropdown.OptionData> listaPortas = new List<Dropdown.OptionData>();

		foreach (string nomePorta in SerialPort.GetPortNames())
			listaPortas.Add(new Dropdown.OptionData(nomePorta));

		listaPortasDropdown.ClearOptions();
		listaPortasDropdown.AddOptions(listaPortas);
	}

	public void ConectarPortaSelecionada()
	{
		try
		{
			string nomePortaSelecionada = listaPortasDropdown.options[listaPortasDropdown.value].text.ToString();

			raspberry = new SerialPort(nomePortaSelecionada, 9600)
			{
				ReadTimeout = 10
			};

			raspberry.Open();

			if (raspberry.IsOpen)
				AdicionarInformacaoConsole("Raspberry conectado na porta " + nomePortaSelecionada);
			else
				AdicionarInformacaoConsole("Conexão com Raspberry falhou para a porta " + nomePortaSelecionada);
		}
		catch (IOException) { }
	}

	private void AdicionarInformacaoConsole(string informacao)
	{
		consoleText.text += informacao + "\n";
	}

	public void LimparConsole()
	{
		consoleText.text = "";
	}
}
