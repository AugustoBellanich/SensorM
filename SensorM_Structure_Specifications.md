# SensorM Structure Specifications

## 1. Introducción

### Objetivo del Documento
Este documento tiene como objetivo definir la estructura y funcionalidades de la aplicación SensorM, estableciendo una guía clara para su desarrollo.

### Alcance
El documento cubre la estructura general de la aplicación, incluyendo la organización del menú, las funcionalidades de cada página, y consideraciones para futuras escalabilidades.

### Público Objetivo
Destinado a desarrolladores, diseñadores UI/UX y stakeholders del proyecto SensorM.

## 2. Estructura de la Aplicación

### Menú Lateral o Pestañas
#### Inicio
Página de bienvenida que muestra un resumen de las funcionalidades más importantes de la aplicación y permite la búsqueda y visualización de dispositivos disponibles.

#### Dispositivos
Sección donde se pueden gestionar los dispositivos conectados, incluyendo la visualización de su estado actual y configuraciones.

#### Calibraciones
Área dedicada para gestionar las calibraciones de los sensores, permitiendo agregar, editar o eliminar curvas de calibración.

#### Base de Datos
Sección donde se pueden visualizar y gestionar los datos almacenados, incluyendo funcionalidades para exportar o eliminar datos.

#### Configuraciones
Página para ajustar las configuraciones generales de la aplicación, como las preferencias de notificación, idioma, entre otros.

### Páginas y Funcionalidades
#### Inicio
- **Buscar Dispositivos:** Botón para iniciar la búsqueda de dispositivos disponibles mediante Bluetooth.
- **Lista de Dispositivos Encontrados:** Área que muestra los dispositivos encontrados durante la búsqueda.
- **Dashboard:** Área que muestra un resumen de los datos más recientes y relevantes.
- **Notificaciones:** Sección para visualizar las alertas y notificaciones importantes.

#### Dispositivos
- **Agregar Nuevo Dispositivo:** Funcionalidad para agregar nuevos dispositivos a la aplicación.
- **Lista de Dispositivos:** Área para visualizar y gestionar los dispositivos agregados.
- **Detalle del Dispositivo:** Al ingresar a un dispositivo se muestra el estado actual del mismo, opciones de lectura, inicio de toma y guardado de datos, importación de datos desde la memoria SD, entre otros.

#### Calibraciones
- **Agregar Nueva Calibración:** Funcionalidad para agregar nuevas curvas de calibración.
- **Lista de Calibraciones:** Área para visualizar y gestionar las calibraciones existentes.

#### Base de Datos
- **Visualizar Datos:** Funcionalidad para visualizar los datos almacenados en diferentes formatos (gráficos, tablas, etc.).
- **Exportar Datos:** Opción para exportar los datos almacenados a diferentes formatos (Excel, CSV, etc.).

#### Configuraciones
- **Preferencias:** Área para ajustar las preferencias de usuario.
- **Ayuda:** Sección con documentación y asistencia para el uso de la aplicación.

## 3. Consideraciones para la Escalabilidad

### Adición de Nuevos Dispositivos
Descripción de cómo la aplicación puede escalar para soportar nuevos dispositivos en el futuro.

### Adaptabilidad de la Base de Datos
Descripción de cómo la base de datos puede adaptarse para soportar nuevos tipos de datos y estructuras.

## 4. Pruebas

### Pruebas de Usabilidad
Descripción de las pruebas de usabilidad que se deben realizar para garantizar una buena experiencia de usuario.

### Pruebas de Funcionalidad
Descripción de las pruebas de funcionalidad para asegurar que todas las características de la aplicación funcionen como se espera.

### Pruebas de Responsividad
Descripción de las pruebas de responsividad para garantizar que la aplicación funcione correctamente en diferentes dispositivos y resoluciones.

## 5. Conclusión

### Resumen
Resumen de los puntos clave discutidos en el documento.

### Próximos Pasos
Descripción de los próximos pasos en el desarrollo de la aplicación.

## 6. Anexos

### Glosario
Lista de términos técnicos utilizados en el documento y sus definiciones.

### Referencias
Lista de referencias y recursos adicionales.

## 7. Historial de Revisiones

| Versión | Fecha       | Descripción del Cambio | Autor |
|---------|-------------|------------------------|-------|
| 1.0     | 10/09/2023  | Creación del documento | Augusto Bellanich |
