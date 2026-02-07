
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace PracticaTP1;

public class AccesoADatosJSON //: IAccesoADatos
{
    string rutaArchivoCadeteria = "Datos/cadeteria.json";
    public Cadeteria CrearCadeteria()
    {
        var cadeteria = new Cadeteria();
        if(File.Exists(rutaArchivoCadeteria))
        {
            string txtJson = File.ReadAllText(rutaArchivoCadeteria);
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(txtJson);
            /* cadeteria.listaCadetes = new List<Cadete>();
            cadeteria.listadoPedidos = new List<Pedidos>(); */
        }
        return cadeteria;
    }

    string rutaArchivoCadetes = "Datos/cadetes.json";
    public List<Cadete> CargarCadetes()
    {
        var cadetes = new List<Cadete>();
        if(File.Exists(rutaArchivoCadetes))
        {
            string txtJson = File.ReadAllText(rutaArchivoCadetes);
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(txtJson);
        }
        return cadetes;
    }
    public void GuardarCadetes(List<Cadete> cadeteNuevo)
    {
        using (FileStream archivo = new FileStream(rutaArchivoCadetes, FileMode.Create))
        {
            using(StreamWriter escribirCadete = new StreamWriter(archivo))
            {
                var archivoGuardar = JsonSerializer.Serialize<List<Cadete>>(cadeteNuevo);
                escribirCadete.WriteLine(archivoGuardar);
            }
        }
    }
    string rutaArchivoPedidos = "Datos/pedidos.json";
    public List<Pedidos> CargarPedidos()
    {
        var pedidos = new List<Pedidos>();
        if(File.Exists(rutaArchivoPedidos))
        {
            string txtJson = File.ReadAllText(rutaArchivoPedidos);
            pedidos = JsonSerializer.Deserialize<List<Pedidos>>(txtJson);
        }
        return pedidos;
    }

    public void GuardarPedidos(List<Pedidos> pedidoNuevo)
    {
        using (FileStream archivo = new FileStream(rutaArchivoPedidos, FileMode.Create))
        {
            using (StreamWriter escribirPedido = new StreamWriter(archivo))
            {
                var archivoAGuardar = JsonSerializer.Serialize<List<Pedidos>>(pedidoNuevo);
                escribirPedido.WriteLine(archivoAGuardar);
            }
        }
    }
}