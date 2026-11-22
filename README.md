# Triaje Médico con Árbol Binario (C# / .NET)

**Autor:** Juan José Lope Sarmiento (código 40938)

Aplicación de consola en C# (.NET 8) que clasifica pacientes de urgencias por puntaje de riesgo (1 a 100) usando un Árbol Binario de Búsqueda.

## Estructura

| Archivo | Responsabilidad |
|---------|-----------------|
| `Clases/Paciente.cs` | Entidad de negocio |
| `Clases/NodoArbol.cs` | Unidad estructural del árbol |
| `Clases/ArbolBinarioTriaje.cs` | Inserción, InOrden, búsqueda, más crítico, dibujo y respaldo |
| `Program.cs` | Menú interactivo |
| `docs/requerimientos.md` | Requerimientos funcionales, no funcionales y de dominio |
| `docs/backlog.md` | Backlog con historias de usuario |

## Menú

1. Registrar nuevo paciente
2. Visualizar jerarquía del árbol
3. Listar pacientes ordenados (InOrden)
4. Consultar paciente más crítico
5. Buscar paciente por puntaje
6. **Respaldar pacientes en archivo TXT**
7. Salir

## Ejecución

1. Abrir `TriajeBinarioApp.csproj` en Visual Studio Community.
2. Ejecutar con Ctrl + F5.
3. El respaldo queda en `bin/Debug/net8.0/respaldo_triaje.txt`.

Con la CLI: `dotnet run`.

## Versión en Java

La migración del mismo problema está en el repositorio `triaje-java`.
