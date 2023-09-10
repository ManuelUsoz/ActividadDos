﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;


namespace DAO
{
    public class ArticuloDAO
    {
        private void LoadArticle(ref Articulo articulo, ref AccesoADatos accesoADatos)
        {
            //Metodo que sirve para cargar el articulo proviniente de la base, con joins a MARCAS Y CATEGORIAS
            articulo.Id = (int)accesoADatos.Lector["Id"];
            articulo.Code = accesoADatos.Lector["Codigo"].ToString();
            articulo.Nombre = accesoADatos.Lector["Nombre"].ToString();
            articulo.Descripcion = accesoADatos.Lector["Descripcion"].ToString();
            articulo.Marca = new Marca();
            articulo.Marca.Descripcion = accesoADatos.Lector["Marca"].ToString();
            articulo.Categoria = new Categoria();
            articulo.Categoria.Descripcion = accesoADatos.Lector["Categoria"].ToString();
            articulo.Precio = (decimal)accesoADatos.Lector["Precio"];
        }
        public List<Articulo> GetArticulos()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
            try
            {
                accesoADatos.AbrirConexion();

                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id";

                accesoADatos.consultar(consulta);
                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    listaArticulos.Add(articulo);
                }

                return listaArticulos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> GetArticulos(int marcaId, int categoriaId)
        {
            try
            {
                AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
                List<Articulo> articulos = new List<Articulo>();

                accesoADatos.consultar("SELECT A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id WHERE M.Id = @MarcaId AND C.Id = @CategoriaId;");
                accesoADatos.setearParametro("@MarcaId", marcaId);
                accesoADatos.setearParametro("@CategoriaId", categoriaId);
                accesoADatos.AbrirConexion();

                accesoADatos.AbrirConexion();

                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    articulos.Add(articulo);

                }
                accesoADatos.cerrarConexion();
                return articulos;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> GetArticulos(int marcaId)
        {

            try
            {
                AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
                List<Articulo> articulos = new List<Articulo>();

                accesoADatos.consultar("SELECT A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A JOIN MARCAS M ON A.IdMarca = M.Id JOIN CATEGORIAS C ON A.IdCategoria = C.Id WHERE M.Id = @MarcaId;");
                accesoADatos.setearParametro("@MarcaId", marcaId);
                accesoADatos.AbrirConexion();
                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    articulos.Add(articulo);

                }
                accesoADatos.cerrarConexion();
                return articulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> GetArticulosByCategoria(int idCategoria)
        {
            try
            {
                AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
                List<Articulo> articulos = new List<Articulo>();

                accesoADatos.consultar("SELECT A.Codigo, A.Nombre, A.Descripcion, C.Descripcion as Categoria, M.Descripcion as Marca, A.Precio FROM ARTICULOS A JOIN CATEGORIAS C ON A.IdCategoria = C.Id JOIN MARCAS M ON A.IdMarca = M.Id WHERE A.IdCategoria = @CategoriaId;");
                accesoADatos.setearParametro("@CategoriaId", idCategoria);
                accesoADatos.AbrirConexion();
                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    articulos.Add(articulo);

                }
                accesoADatos.cerrarConexion();
                return articulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> GetArticulos(string field, string criteria, string value)
        {
            AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
            List<Articulo> articulos = new List<Articulo>();

            //std::map<> equivalente HashMap equivalente
            Dictionary<string, string> operadoresSQL = new Dictionary<string, string>
            {
                { "Es igual a", "=" },
                { "Es mayor a", ">" },
                { "Es menor a", "<" },
                { "Contiene", "LIKE '%" },
                { "Igual a", "=" }
            };

            string sqloperator = operadoresSQL[criteria];
            string query;
            if (field == "Precio")
            {
                query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE " + field + " " + sqloperator + " " + value + "";
            }
            else
            {
                if(sqloperator == "LIKE '%")
                {
                    query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE " + field + " " + sqloperator + "" + value + "%'";
                }
                else
                {
                    query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE " + field + " " + sqloperator + " '" + value + "'";

                }
            }

            
            try
            {
                accesoADatos.consultar(query);
                accesoADatos.AbrirConexion();
                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    articulos.Add(articulo);
                }

                return articulos;
            }catch (Exception ex)
            {
                throw ex;
            }finally
            {
                accesoADatos.cerrarConexion();
            }
        }

        public List<Articulo> GetArticulosByHint(string hint)
        {
            AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
            List<Articulo> articulos = new List<Articulo>();
            try
            {
                string query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A.Nombre LIKE '%" + hint +"%' OR A.Codigo LIKE '%" + hint +"%' OR A.Descripcion LIKE '%" + hint +"%' OR M.Descripcion LIKE '%" + hint +"%' OR C.Descripcion LIKE '%" + hint +"%';";
                accesoADatos.consultar(query);
                accesoADatos.AbrirConexion();
                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    articulos.Add(articulo);
                }

                return articulos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoADatos.cerrarConexion();
            }
        }
        public List<Articulo> GetArticulos(int idMarca, string field, string criteria, string value)

        {
            AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
            List<Articulo> articulos = new List<Articulo>();
            Dictionary<string, string> operadoresSQL = new Dictionary<string, string>
            {
                { "Es igual a", "=" },
                { "Es mayor a", ">" },
                { "Es menor a", "<" },
                { "Contiene", "LIKE '%" },
                { "Igual a", "=" }
            };
            try
            {
                string sqloperator = operadoresSQL[criteria];
                string query = "";

                if (field == "Precio")
                {
                    query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + " " + value + " AND A.IdMarca = "+ idMarca + "";
                }
                else
                {
                    if (sqloperator == "LIKE '%")
                    {
                        query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + "" + value + "%' AND A.IdMarca = "+idMarca+"";
                    }
                    else
                    {
                        query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + " '" + value + "' AND A.IdMarca = " + idMarca + "";

                    }
                }

                accesoADatos.consultar(query);
                accesoADatos.AbrirConexion();
                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    articulos.Add(articulo);
                }

                return articulos;

            }
            catch (Exception ex) {
                throw ex;
            }
            finally
            {
                accesoADatos.cerrarConexion();
            }
        }

        public List<Articulo> GetArticulos(int idMarca, int categoriaId, string field, string criteria, string value)
        {
            AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
            List<Articulo> articulos = new List<Articulo>();
            Dictionary<string, string> operadoresSQL = new Dictionary<string, string>
            {
                { "Es igual a", "=" },
                { "Es mayor a", ">" },
                { "Es menor a", "<" },
                { "Contiene", "LIKE '%" },
                { "Igual a", "=" }
            };
            try
            {
                string sqloperator = operadoresSQL[criteria];
                string query = "";

                if (field == "Precio")
                {
                    query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + " " + value + " AND A.IdCategoria = " + categoriaId + " AND A.IdMarca = "+idMarca+"";
                }
                else
                {
                    if (sqloperator == "LIKE '%")
                    {
                        query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + "" + value + "%' AND A.IdCategoria = " + categoriaId + " AND A.IdMarca = " + idMarca + "";
                    }
                    else
                    {
                        query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + " '" + value + "' AND A.IdCategoria = " + categoriaId + " AND A.IdMarca = " + idMarca + "";

                    }
                }

                accesoADatos.consultar(query);
                accesoADatos.AbrirConexion();
                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    articulos.Add(articulo);
                }

                return articulos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoADatos.cerrarConexion();
            }
        }

        public List<Articulo> GetArticulosByCategory(int categoryId, string field, string criteria, string value)
        {
            AccesoADatos accesoADatos = new AccesoADatos("server=.; database=CATALOGO_P3_DB; integrated security=true");
            List<Articulo> articulos = new List<Articulo>();
            Dictionary<string, string> operadoresSQL = new Dictionary<string, string>
            {
                { "Es igual a", "=" },
                { "Es mayor a", ">" },
                { "Es menor a", "<" },
                { "Contiene", "LIKE '%" },
                { "Igual a", "=" }
            };
            try
            {
                string sqloperator = operadoresSQL[criteria];
                string query = "";

                if (field == "Precio")
                {
                    query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + " " + value + " AND A.IdCategoria = " + categoryId + "";
                }
                else
                {
                    if (sqloperator == "LIKE '%")
                    {
                        query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + "" + value + "%' AND A.IdCategoria = " + categoryId + "";
                    }
                    else
                    {
                        query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A." + field + " " + sqloperator + " '" + value + "' AND A.IdCategoria = " + categoryId + "";

                    }
                }

                accesoADatos.consultar(query);
                accesoADatos.AbrirConexion();
                accesoADatos.ejecutarLectura();

                while (accesoADatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    this.LoadArticle(ref articulo, ref accesoADatos);
                    articulos.Add(articulo);
                }

                return articulos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoADatos.cerrarConexion();
            }
        }
        
        public void agregar(Articulo nuevo)
        {
            AccesoADatos datos = new AccesoADatos();
            try
            {
                datos.consultar("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES ('"+nuevo.Code+"', '"+nuevo.Nombre+"', '"+nuevo.Descripcion+"', @IdMarca, @IdCategoria, "+nuevo.Precio+")");
                datos.setearParametro("@IdMarca",nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria",nuevo.Categoria.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }  
    }
}
