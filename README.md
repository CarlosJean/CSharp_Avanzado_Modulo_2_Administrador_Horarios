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

## Imágenes

### Diagrama de base de datos
<img width="834" height="672" alt="imagen" src="https://github.com/user-attachments/assets/90e4e40c-bcbd-4185-84c6-6ad725c839ae" />


### Materias
<img width="1920" height="1032" alt="65" src="https://github.com/user-attachments/assets/7059b3b8-cb03-45b7-81e2-1b96d7eb94a2" />

### Maestros
<img width="1920" height="1032" alt="7" src="https://github.com/user-attachments/assets/f6684ede-f29e-45be-98a3-8cb41a80b09c" />

### Grados
<img width="1920" height="1032" alt="5" src="https://github.com/user-attachments/assets/fb5c485a-4fcb-47ab-81fb-bf64bceb7707" />

---
### Horarios
<img width="1920" height="1032" alt="1" src="https://github.com/user-attachments/assets/bda71889-0ced-499f-94f2-4c7fe85cb9cf" />
<img width="1920" height="1032" alt="3" src="https://github.com/user-attachments/assets/4c852389-8c94-4339-a462-13c1e4cc027e" />

#### Validación de choque de horarios

Un mismo profesor no puede estar en dos lugares al mismo tiempo.
<img width="1920" height="1032" alt="4" src="https://github.com/user-attachments/assets/9692e998-5d1f-4623-90ff-a4bca84f04fc" />

#### Creación de horarios
<img width="1920" height="1032" alt="2" src="https://github.com/user-attachments/assets/dfb2bca3-c82f-4056-8743-4f653eb4cc3c" />





