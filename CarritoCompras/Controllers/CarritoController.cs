﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarritoCompras.Models;

namespace CarritoCompras.Controllers
{
    public class CarritoController : Controller
    {
        //
        // GET: /Carrito/
        private CarritoBDEntities ce = new CarritoBDEntities();

        public ActionResult AgregaCarrito(int id)
        {
            if (Session["carrito"] == null)
            {
                List<CarritoItem> compras = new List<CarritoItem>();
                compras.Add(new CarritoItem(ce.Producto.Find(id),1));
                Session["carrito"] = compras;
            }
            else
            {
                List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
                int IndexExistente = getIndex(id);
                if (IndexExistente == -1)
                    compras.Add(new CarritoItem(ce.Producto.Find(id), 1));
                else
                    compras[IndexExistente].Cantidad++;
                Session["carrito"] = compras;
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            compras.RemoveAt(getIndex(id));
            Session["carrito"] = compras;
            return View("AgregaCarrito");
        }

        private int getIndex(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            for (int i = 0; i < compras.Count; i++)
            {
                if (compras[i].Producto.Id == id)
                    return i;
            }
            return -1;
        }

    }
}
