# Atención en hospitales Covid-19

## Introducción

## Requerimientos

## Instalación y Configuración 

## Funcionamiento 

* Colocar imagen menú principal. 

 **Menú principal:** Es la página principal del programa, en esta vista se encuentran las secciones disponibles. Cada una lo llevarán a un menú secundario con diferentes opciones.  

### 1.	Registro  

* Imagen Registro

En esta sección el usuario podrá ingresar los datos del paciente. Se le solicita el nombre, apellido, DPI o Cui (dependiendo de la edad del paciente), departamento de residencia, municipio, síntomas que presenta, la posible causa de contagio, 

categoría del paciente (sospechoso o confirmado) y la característica (el rango de edad al que pertenece). 

Esta información se almacenará en la base de datos del programa para llevar registro de los ingresados. Adicionalmente, se enviará a los pacientes a una cola dependiendo de la categoría y se le asignará camilla a las personas contagiadas si estuvieran disponibles. 

### 2.	Búsquedas

* Imagen Busquedas

El presionar el botón de "Búsquedas" se desplegará una vista en la que podrá seleccionar el parámetro por el que quiere buscar a un pacientes (Nombre, apellido o DPI/Cui) y además deberá ingresar el valor buscado. Al pulsar Buscar, el programa le mostrará las coincidencias de búsqueda en el siguiente formato: 

* Imágen búsquedas tablas. 

En esta tabla se le mostrará toda la información ingresada en el Registro de pacientes y también el estado en el que se encuentra el paciente actualmente. Podrá observar pacientes en estado "contagiado", "sospechoso" y/o "recuperado".

### 3.	Estadísticas 
* Estadisticas Generales Imagen

En esta pestaña, el usuario observará la gráfica general de datos de ingreso y egreso de pacientes. Se muestran los datos de las personas contagiadas, sospechosas y recuperadas que han ingresado al sistema de hospitales. La cantidad de sospechosos es la suma de sospechosos contagiados y no contagiados, y los pacientes se suman a cada una de las categorías dependiendo de los resultados del examen. 

En este espacio podrá encontrar dos funcionalidades extra: 
**a)	Gráfica comparativa de hospitales**
	Esta gráfica compara la cantidad de pacientes contagiados, sospechosos y 	egresados que hay en cada hospital. Se muestran los cinco hospitales en la 	misma gráfica. 
	
**b)	Buscar por hospital **
	En este separado puede seleccionar un hospital en específico del que se 	quieran ver los datos listados anteriormente, se mostrará una gráfica similar a la general, pero con los datos de un solo hospital (el 	seleccionado).

* Imagen comparativa hospitales

**Vista de gráfica comparativa de hospitales**

* Imagen gráfica por hospital

### 4.	Hospitales

Al seleccionar esta opción, el programa le desplegará un menú de hospitales donde se encuentran los cinco disponibles y cada hospital mostrará tres opciones:

* Imagen lista de hopitales

* Imagen menu hopitales

**a)	Camas disponibles y ocupadas**
Se muestra una tabla con las camillas que se encuentras ocupadas por un paciente y las que están disponibles, así como el código y el número de 	camilla en el hospital.

* Imagen camas

**b)	Realizar una prueba **
Al realizar una prueba, el programa analiza la probabilidad que tiene un 	paciente de estar contagiado y muestra el resultado de la prueba, para 	luego enviarlo a la cola de contagiados (o camilla si hubiera una 	disponible) o descartarlo. 

* Imagen Alert con resultado prueba. 

Los mensajes que pueden mostrar la aplicación son: 

* •	El paciente que era sospechoso y seguía en la cola ha resultado positivo para el Covid - 19
* •	La prueba ha salido negativa
* •	No se puede realizar la prueba, no hay sospechosos en la cola.

Como usuario puede navegar entra las diferentes secciones principales del programa en la parte superior presionando la funcionalidad a la que se quiere dirigir.

### Solución de problemas y preguntas frecuentes 




