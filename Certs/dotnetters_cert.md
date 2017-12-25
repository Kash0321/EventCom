# Cómo generar un certificado
La herramienta está en el JDK de Java, por ejemplo en `'C:\Program Files (x86)\Java\jdk1.8.0_152\bin\keytool.exe'`
```sh
keytool -genkey -v -keystore [my-key].keystore -alias [alias_name] -keyalg RSA -keysize 2048 -validity 10000 
```
# Generación del certificado dotnetters.keystore
## Generación y datos utilizados y relevantes
### Comando
```sh
keytool -genkey -v -keystore c:\dotnetters.keystore -alias dtnkey -keyalg RSA -keysize 2048 -validity 10000
```
### Datos indicados durante la ejecución del comando de generación (keytool)
* Contraseña del almacén de claves: **[NOLAVOYAESCRIBIRAQUÍ]**
* ¿Cuáles son su nombre y su apellido? **DotNetters Zaragoza**
* ¿Cuál es el nombre de su unidad de organización? **DEV**
* ¿Cuál es el nombre de su organización? **DotNetters**
* ¿Cuál es el nombre de su ciudad o localidad? **Zaragoza**
* ¿Cuál es el nombre de su estado o provincia? **Zaragoza**
* ¿Cuál es el código de país de dos letras de la unidad? **ES**
* Contraseña de clave para `<kkjkey>`: **[NOLAVOYAESCRIBIRAQUÍ]**
### Datos a rellenar para la compilación en el App Center:
- Keystore password: **[NOLAVOYAESCRIBIRAQUÍ]**
- Key alias: **dtnkey**
- Key password: **[NOLAVOYAESCRIBIRAQUÍ]**
## Webliografía
[Guía online](https://coderwall.com/p/r09hoq/android-generate-release-debug-keystores)
