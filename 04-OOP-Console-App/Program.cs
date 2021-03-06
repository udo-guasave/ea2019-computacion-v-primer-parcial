﻿using System;
using System.Collections.Generic;
using System.Linq;
using _04_OOP_Console_App.Models;
using Microsoft.EntityFrameworkCore;

namespace _04_OOP_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: PrimerParcial
            // Mostrar todas las calificaciones de todos los periodos del alumno con id 1
            // Deberá incluir en la lista a mostrar, el nombre del alumno, docente, materia,
            // carrera y las calificaciones. Para antes de la siguiente clase.
            // nombrar al commit como PrimerParcial EFCore
            PrimerParcial();
        }

        private static void PrimerParcial()
        {
            using (var db = new UdoDbContext())
            {
                var calificaciones = db.Calificaciones
                                        .Include(c => c.Alumno)
                                            .ThenInclude(c => c.Carrera)
                                        .Include(c => c.Docente)
                                        .Include(c => c.Materia)
                                        .Where(c => c.AlumnoId == 1)
                                        .ToList();

                foreach (var cal in calificaciones)
                {
                    Console.WriteLine($"Carrera: {cal.Alumno.Carrera.Nombre}, Alumno: {cal.Alumno.NombreCompleto}, Docente: {cal.Docente.Nombre}, Materia: {cal.Materia.Nombre}, {cal.Periodo}: {cal.Nota}");
                }
            }
        }

        private static void MostrarAlumnosCarreras()
        {
            using (var db = new UdoDbContext())
            {
                var alumnos = db.Alumnos.Include(a => a.Carrera);
                foreach (var alumno in alumnos)
                {
                    Console.WriteLine($"Alumno: {alumno.NombreCompleto}, Carrera: {alumno.Carrera.Nombre}");
                }
            }
        }

        private static void EliminarCarrera(Carrera carrera)
        {
            using (var db = new UdoDbContext())
            {
                db.Carreras.Remove(carrera);
                db.SaveChanges();
                ImprimirCarreras();
            }
        }

        private static void ActualizarCarrera(Carrera carrera)
        {
            using (var db = new UdoDbContext())
            {
                db.Carreras.Update(carrera);
                db.SaveChanges();
                ImprimirCarreras();
            }
        }

        private static Carrera BuscarCarreraPorId(int carreraId)
        {
            using (var db = new UdoDbContext())
            {
                var carrera = db.Carreras.Find(carreraId);
                return carrera;
            }
        }

        private static void ImprimirCarreras()
        {
            using (var db = new UdoDbContext())
            {
                var carreras = db.Carreras;
                foreach (var c in carreras)
                {
                    Console.WriteLine($"Nombre: {c.Nombre}, Plan: {c.Plan}");
                }
            }
        }

        private static void CrearCarreras()
        {
            using (var db = new UdoDbContext())
            {
                db.Database.EnsureCreated();

                var carreras = new List<Carrera>
                {
                    new Carrera()
                    {
                        Nombre = "Contabilidad",
                        Plan = Plan.Trimestral
                    },
                    new Carrera()
                    {
                        Nombre = "Administración y finanzas",
                        Plan = Plan.Semestral
                    },
                    new Carrera()
                    {
                        Nombre = "Ingeniería civil",
                        Plan = Plan.Trimestral
                    }
                };
                // var carrera1 = new Carrera()
                // {
                //     Nombre = "Contabilidad",
                //     Plan = Plan.Trimestral
                // };
                // var carrera2 = new Carrera()
                // {
                //     Nombre = "Administración y finanzas",
                //     Plan = Plan.Semestral
                // };
                // var carrera3 = new Carrera()
                // {
                //     Nombre = "Ingeniería civil",
                //     Plan = Plan.Trimestral
                // };

                // db.Add(carrera1);
                // db.Add(carrera2);
                // db.Add(carrera3);

                db.AddRange(carreras);

                db.SaveChanges();
            }
        }
    }
}
