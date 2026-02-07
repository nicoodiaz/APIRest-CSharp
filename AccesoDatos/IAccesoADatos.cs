namespace PracticaTP1;

public interface IAccesoADatos
{
    public Cadeteria CrearCadeteria(string rutaArchivoCadeteria);
    public List<Cadete> CargarCadetes(string rutaArchivoCadetes);

}