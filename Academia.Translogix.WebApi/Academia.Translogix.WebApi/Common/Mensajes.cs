using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi.Common
{
    public class Mensajes
    {
        public const string _01_Operacion_Exitosa = "Operacion realizada exitosamente";
        public const string _02_Registros_Obtenidos = "Registros obtenidos con éxito";
        public const string _03_Error_Registros_Obtenidos = "Error al obtener registros: ";
        public const string _04_Registros_No_Encontrado = "Registro no encontrado";
        public const string _05_Error_Buscar_Registro = "Error al buscar registro:  ";
        public const string _06_Valores_Nulos = "No se aceptan valores nulos";
        public const string _07_Registro_Guardado = "Registro guardado con éxito";
        public const string _08_Error_Guardado = "Erro al guardar: ";
        public const string _09_Registro_Actualizado = "Registro actualizado con éxito";
        public const string _10_Error_Actualizado = "Erro al actualizar: ";
        public const string _11_Propiedad_Activo_No_Encontrada = "El registro no tiene la propiedad es_activo";
        public const string _12_Registro_Eliminado = "Registro eliminado con éxito";
        public const string _13_Error_Eliminado = "Error al eliminar: ";
        public const string _14_Error_Buscar_Registro = "Error al buscar registro:  ";
        public const string _15_Error_Operacion = "No se pudo realizar la operacion";
        public const string _16_Colaborador_Misma_Identidad = "Ya existe un colaborador con la misma identidad";
        public const string _16_Colaborador_Mismo_Correo = "Ya existe un colaborador con el mismo correo";
        public const string _17_Colaborador_Sucursal_No_Encontrado = "Colaborador o sucursal no encontrados";
        public const string _18_Colaborador_Sucursal_Duplicada = "No se puede asignar un colaborador a la misma sucursal 2 veces para el colaborador: {0} {1}";
        public const string _19_Colaborador_Insertado = "Registro insertado correctamente para el colaborador: {0} {1}";
        public const string _20_Error_Colaborador_Insertar = "No se pudo insertar correctamente para el colaborador: {0} {1}";
        public const string _21_Colaborador_Distancia_Invalida = "La distancia entre la casa del colaborador no puede ser 0 ni mayor de 50km para el colaborador: {0} {1}";
        public const string _22_Sucursal_No_Encontrada = "No se encontro ninguna sucursal con ese Id";
        public const string _23_Transportista_No_Encontrada = "No se encontro ningun transportista con ese Id";
        public const string _24_Usuario_No_Encontrado = "No se encontro ningun Usuario con ese Id";
        public const string _25_Usuario_Administrador = "Solo los usuarios que son administrador pueden registrar viajes";
        public const string _26_Ubicacion_Requerida = "Se requiere al menos una ubicacion";
        public const string _27_Validar_Sexo_F_M = "El sexo debe ser masculino o femenino";
        public const string _28_Usuario_Invalido = "Usuario invalido.";
    }
}