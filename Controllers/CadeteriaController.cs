using Microsoft.AspNetCore.Mvc;
using PracticaTP1;

namespace APIRest_CSharp.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly Cadeteria miCadeteria;
    private AccesoADatosJSON accesoDatosJSON;
    public CadeteriaController()
    {
        accesoDatosJSON = new AccesoADatosJSON();
        miCadeteria = accesoDatosJSON.CrearCadeteria();
        miCadeteria.listaCadetes = accesoDatosJSON.CargarCadetes();
        miCadeteria.listadoPedidos = accesoDatosJSON.CargarPedidos();
    }

    [HttpGet("Cadeteria")]
    public IActionResult GetCadeteria()
    {
        string informacion = $"{miCadeteria.nombreCadeteria} | {miCadeteria.telefonoCadeteria}";
        return Ok(informacion);
    }
    [HttpGet("Pedidos")]
    public IActionResult GetPedidos()
    {
        return Ok(miCadeteria.ObtenerPedidos());
    }
    [HttpGet("Cadetes")]
    public IActionResult GetCadetes()
    {
        var listadoCadetes = miCadeteria.ObtenerCadetes();
        if(listadoCadetes != null) return Ok(miCadeteria.ObtenerCadetes());
        return NotFound();
    }
    [HttpPost("AgregarPedido")]
    public IActionResult AgregarPedido([FromBody] Pedidos nuevoPedido)
    {
        var pedido = miCadeteria.DarAltaPedido(nuevoPedido.nroPedido, nuevoPedido.observacion, nuevoPedido.cliente);
        if(pedido == null) return BadRequest("Fallo al crear pedido");
        accesoDatosJSON.GuardarPedidos(miCadeteria.listadoPedidos);
        return Created("AgregarPedido", nuevoPedido);
    }
    [HttpPost("AgregarCadete")]
    public IActionResult AgregarCadete([FromBody] Cadete nuevoCadete)
    {
        if (miCadeteria.AgregarCadete(nuevoCadete))
        {
            accesoDatosJSON.GuardarCadetes(miCadeteria.listaCadetes);
            return Ok("Cadete guardado");
        }
        return BadRequest("No se creo el cadete");
    }
    [HttpPut("AsignarPedido")]
    public IActionResult AsignarPedido(int nroPedido, int idCadete)
    {
        var pedido = miCadeteria.BuscarUnPedidoPorId(nroPedido);
        if (pedido != null)
        {
            var cadete = miCadeteria.BuscarCadetePorID(idCadete);
            if (cadete == null) return BadRequest("No se encontro cadete");
            if(miCadeteria.AsignarPedido(cadete.id, pedido))
            {
                accesoDatosJSON.GuardarPedidos(miCadeteria.listadoPedidos);
                return Ok("Pedido Asignado");
            }
        }
        return BadRequest("No se encontro pedido");
    }
    [HttpPut("CambiarEstadoPedido")]
    public IActionResult CambiarEstadoPedido(int nroPedido, Estado nuevoEstado)
    {
        if(miCadeteria.CambiarEstadoPedido(nroPedido, nuevoEstado)) 
        {
            accesoDatosJSON.GuardarPedidos(miCadeteria.listadoPedidos);
            return Ok("Estado cambiado");
        }
        return BadRequest("No se cambio el estado");
    }
    [HttpPut("CambiarCadete")]
    public IActionResult CambiarCadetePedido(int nroPedido, int idNuevoCadete)
    {
        if(miCadeteria.ReasignarPedido(nroPedido, idNuevoCadete)) 
        {
            accesoDatosJSON.GuardarPedidos(miCadeteria.listadoPedidos);
            return Ok("Pedido reasignado");
        }
        return BadRequest("Pedido NO reasignado");
    }
}