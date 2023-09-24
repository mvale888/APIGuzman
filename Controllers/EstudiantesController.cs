using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace APIEstudiantes.Controllers;

[ApiController]
[Route("api/estudiantes")]
public class EstudiantesController : ControllerBase
{
    //creo una lista con objetos Estudiante del Models
    public static List<Estudiante> estudiantesLista = new List<Estudiante>() { };
    string message = "";
    //controla que tenga un logueo autorizado, pero no está implementada esta función aún.
    private readonly ILogger<EstudiantesController> _logger;
    public EstudiantesController(ILogger<EstudiantesController> logger)
    {
        _logger = logger;
    }

    //get que trae todos los registros almacenados
    [HttpGet]
    public IEnumerable<Estudiante> Get()
    {
        return estudiantesLista;
    }

    //este get solo trae el registro que coincida con el id indicado en la ruta
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Estudiante> Get(int id)
    {
        int estudianteBuscado = estudiantesLista.FindIndex(r => r.Id == id);
        if (estudianteBuscado < 0) //si no existe el id ingresado, devuelve un notfound
        {
            message="El id ingresado no existe";
            return NotFound();
        }
        return estudiantesLista[estudianteBuscado];
    }

    //ingreso un nuevo registro de estudiante
    [HttpPost]
    public dynamic Post(Estudiante estudiante)
    {
        estudiante.Id = 1;
        if (estudiantesLista.Count > 0)
            estudiante.Id = estudiantesLista.Max(r => r.Id) + 1;
                estudiantesLista.Add(estudiante);
                message = "Cliente registrado";
            return message;
    }

    [HttpPut("{id}")]
    public dynamic Put(int id, Estudiante estudiante)
    {
        int estudianteIndex = estudiantesLista.FindIndex(r => r.Id == id);
        if (estudianteIndex < 0)
        {
            return NotFound();
        }
        estudiantesLista[estudianteIndex] = estudiante; //aquí verificar porque el ID queda en 0 (cero)
        message="Estudiante Id: " + estudianteIndex +" actualizado." ;
        return message;
       // return new EmptyResult();
    }
   
   //vamos a eliminar un estudiente en particular
    [HttpDelete("{id}")]
    
    public dynamic Delete(int id)
    {
        int estudianteIndex = estudiantesLista.FindIndex(r => r.Id == id);
        if (estudianteIndex < 0)
        {
            message="El ID de estudiante no existe";
            return message;
        }
        estudiantesLista.RemoveAt(estudianteIndex);
        message="El estudiante fué eliminado exitosamente de la lista";
        return message;
    }
}