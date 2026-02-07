using System.Data;

namespace PracticaTP1;

public class Cliente
{
    public string nombreCliente { get; set; }
    public string direccionCliente { get; set;}
    public string telefonoCliente { get; set; }
    public string datosReferencia { get; set;}

    public Cliente()
    {
        
    }
    public Cliente(string nombre, string direccion, string telefono, string datos)
    {
        this.nombreCliente = nombre;
        this.direccionCliente = direccion;
        this.telefonoCliente = telefono;
        this.datosReferencia = datos;
    }
}