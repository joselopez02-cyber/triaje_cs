using System;
using System.Collections.Generic;
using System.IO;

namespace TriajeBinarioApp.Clases
{
    public class ArbolBinarioTriaje
    {
        public NodoArbol? Raiz { get; private set; }

        public ArbolBinarioTriaje()
        {
            Raiz = null;
        }

        // Inserción recursiva según PuntajeRiesgo
        public void Insertar(Paciente nuevoPaciente)
        {
            Raiz = InsertarRecursivo(Raiz, nuevoPaciente);
        }

        private NodoArbol InsertarRecursivo(NodoArbol? actual, Paciente paciente)
        {
            if (actual == null)
            {
                return new NodoArbol(paciente);
            }

            if (paciente.PuntajeRiesgo < actual.Datos.PuntajeRiesgo)
            {
                actual.Izquierdo = InsertarRecursivo(actual.Izquierdo, paciente);
            }
            else
            {
                actual.Derecho = InsertarRecursivo(actual.Derecho, paciente);
            }

            return actual;
        }

        // Recorrido InOrden (Izquierda -> Raíz -> Derecha)
        public void MostrarInOrden()
        {
            if (Raiz == null)
            {
                Console.WriteLine("El árbol se encuentra vacío.");
                return;
            }
            RecorrerInOrden(Raiz);
        }

        private void RecorrerInOrden(NodoArbol? nodo)
        {
            if (nodo == null) return;

            RecorrerInOrden(nodo.Izquierdo);
            Console.WriteLine(nodo.Datos.ToString());
            RecorrerInOrden(nodo.Derecho);
        }

        // Devuelve los pacientes ordenados de menor a mayor gravedad (InOrden)
        public List<Paciente> ObtenerInOrden()
        {
            var lista = new List<Paciente>();
            LlenarInOrden(Raiz, lista);
            return lista;
        }

        private void LlenarInOrden(NodoArbol? nodo, List<Paciente> lista)
        {
            if (nodo == null) return;

            LlenarInOrden(nodo.Izquierdo, lista);
            lista.Add(nodo.Datos);
            LlenarInOrden(nodo.Derecho, lista);
        }

        // Respaldo en archivo TXT (un paciente por línea, separado por |)
        // Retorna la cantidad de pacientes respaldados.
        public int GuardarRespaldoTxt(string ruta)
        {
            List<Paciente> pacientes = ObtenerInOrden();

            using (var escritor = new StreamWriter(ruta, false))
            {
                escritor.WriteLine("RESPALDO TRIAJE MEDICO - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                escritor.WriteLine("Total pacientes: " + pacientes.Count);
                escritor.WriteLine("Riesgo|Nombre|Cedula|Sintoma");
                foreach (Paciente p in pacientes)
                {
                    escritor.WriteLine($"{p.PuntajeRiesgo}|{p.Nombre}|{p.Cedula}|{p.Sintoma}");
                }
            }
            return pacientes.Count;
        }

        // Búsqueda binaria por puntaje de riesgo
        public Paciente? BuscarPorRiesgo(int puntaje)
        {
            return BuscarRecursivo(Raiz, puntaje);
        }

        private Paciente? BuscarRecursivo(NodoArbol? actual, int puntaje)
        {
            if (actual == null) return null;
            if (actual.Datos.PuntajeRiesgo == puntaje) return actual.Datos;

            if (puntaje < actual.Datos.PuntajeRiesgo)
                return BuscarRecursivo(actual.Izquierdo, puntaje);
            else
                return BuscarRecursivo(actual.Derecho, puntaje);
        }

        // Obtener paciente más crítico (extremo derecho del árbol)
        public Paciente? ObtenerMasCritico()
        {
            if (Raiz == null) return null;

            NodoArbol actual = Raiz;
            while (actual.Derecho != null)
            {
                actual = actual.Derecho;
            }
            return actual.Datos;
        }

        // Visualización jerárquica del árbol en consola
        public void ImprimirEstructura()
        {
            if (Raiz == null)
            {
                Console.WriteLine("El árbol está vacío.");
                return;
            }
            ImprimirNodo(Raiz, "", true, "RAÍZ");
        }

        private void ImprimirNodo(NodoArbol? nodo, string sangria, bool esUltimo, string rama)
        {
            if (nodo == null) return;

            Console.Write(sangria);
            Console.Write(esUltimo ? "└── " : "├── ");
            Console.WriteLine($"[{rama}] {nodo.Datos.Nombre} (Riesgo: {nodo.Datos.PuntajeRiesgo})");

            sangria += esUltimo ? "    " : "│   ";

            if (nodo.Izquierdo != null || nodo.Derecho != null)
            {
                if (nodo.Izquierdo != null)
                    ImprimirNodo(nodo.Izquierdo, sangria, nodo.Derecho == null, "IZQ <");

                if (nodo.Derecho != null)
                    ImprimirNodo(nodo.Derecho, sangria, true, "DER >=");
            }
        }
    }
}
