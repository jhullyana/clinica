
using Microsoft.EntityFrameworkCore;


public static class PacientesApi {
    public static void MapPacientesApi(this WebApplication app)
    {

        var group = app.MapGroup("/pacientes");


        group.MapGet("/", async (BancoDeDados db) =>
            //select * from pacientes
            await db.Pacientes.ToListAsync()
        );

        group.MapPost("/", async (Paciente paciente, BancoDeDados db) =>{
            db.Pacientes.Add(paciente);
            //insert into...
            await db.SaveChangesAsync();
        return Results.Created($"/pacientes/{paciente.Id}", paciente);
        }
        );

        group.MapPut("/{id}", async (int id, Paciente pacienteAlterada, BancoDeDados db) =>
        {
            //select * from medicos where id = ?
            var paciente = await db.Pacientes.FindAsync(id);
            if (paciente is null)
            {
                return Results.NotFound();
            }
            paciente.Nome = pacienteAlterada.Nome;
            paciente.Telefone = pacienteAlterada.Telefone;
            paciente.Email = pacienteAlterada.Email;
            paciente.CPF = pacienteAlterada.CPF;

            //update....
            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

    }
}