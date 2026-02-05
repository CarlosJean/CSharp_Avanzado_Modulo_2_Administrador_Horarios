#  Schedule Manager

Aplicación para la gestión y administración de horarios. Este sistema permite organizar agendas y turnos de manera centralizada, utilizando **.NET** y **Entity Framework Core**.

---

##  Configuración e Instalación Local

Sigue estos pasos detallados para configurar el entorno de desarrollo en tu máquina:

### 1. Clonar el repositorio
Primero, descarga el proyecto a tu equipo local:

```bash
git clone https://github.com/CarlosJean/CSharp_Avanzado_Modulo_2_Administrador_Horarios
cd schedule-manager
```

### 2. Configurar la Cadena de Conexión

Para que la aplicación funcione, debe conectarse a tu instancia de SQL Server.

1. Abre el archivo appsettings.json en la raíz del proyecto.

1. Localiza la sección ConnectionStrings.

1. Sustituye el placeholder {your_server_name} por el nombre de tu servidor (ej. . , localhost o el nombre de tu instancia de SQL).

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server={your_server_name};Database=ScheduleManager;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 3. Ejecutar Migraciones de Base de Datos

Es necesario crear la base de datos ScheduleManager y sus tablas correspondientes antes de iniciar la app. Ejecuta el siguiente comando en la terminal (dentro de la carpeta del proyecto):
Bash

```bash
dotnet ef database update -p ScheduleManager
```
