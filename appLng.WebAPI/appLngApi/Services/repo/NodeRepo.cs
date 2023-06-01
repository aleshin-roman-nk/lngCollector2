﻿using Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public class NodeRepo
    {
        private readonly DbFactory _factory;

        public NodeRepo(DbFactory factory) 
        {
            this._factory = factory;
        }

        public IEnumerable<Node> GetAllOf(int terrId)
        {
            using (var db = _factory.Create())
            {
                return db.Nodes.Where(x => x.terrianId == terrId).ToArray();
            }
        }

        /*
         * Если будет какая то доп сложная информация, сделаем метод GetDetail, которая будет возвращает единичный экземпляр Node и доп данные.
         */
        public Node GetDetail(int nodeId)
        {
            throw new NotImplementedException();
        }

        public Node Create(int terrId, Node node)
        {
            using (var db = _factory.Create())
            {
                node.id = 0;

                node.terrianId = terrId;

                db.Nodes.Add(node);
                db.SaveChanges();
                return node;
            }
        }

        public Node Update(int nodeId, Node node)
        {
            using (var db = _factory.Create())
            {
                try
                {
                    var _node = db.Nodes.FirstOrDefault(x => x.id == nodeId);
                    if (_node != null)
                    {
                        _node.name = node.name;
                        _node.description = node.description;
                        _node.y = node.y;
                        _node.x = node.x;

                        db.SaveChanges();
                        return _node;
                    }

                    throw new InvalidOperationException($"Not such Node in db with id = {nodeId}");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        public void Delete(int nodeId)
        {
            using (var db = _factory.Create())
            {
                db.Nodes.Remove(new Node { id = nodeId });
                db.SaveChanges();
            }
        }
    }
}