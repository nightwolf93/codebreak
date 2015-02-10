﻿using Codebreak.Framework.Database;
using Codebreak.Service.World.Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebreak.Service.World.Database.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MonsterGradeRepository : Repository<MonsterGradeRepository, MonsterGradeDAO>
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<int, MonsterGradeDAO> m_gradeById;

        /// <summary>
        /// 
        /// </summary>
        public MonsterGradeRepository()
        {
            m_gradeById = new Dictionary<int, MonsterGradeDAO>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MonsterGradeDAO GetById(int id)
        {
            return m_gradeById[id];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grade"></param>
        public override void OnObjectAdded(MonsterGradeDAO grade)
        {
            m_gradeById.Add(grade.Id, grade);

            MonsterRepository.Instance.GetById(grade.MonsterId).AddGrade(grade);
        }

        public override void UpdateAll(MySql.Data.MySqlClient.MySqlConnection connection, MySql.Data.MySqlClient.MySqlTransaction transaction)
        {
            // NO UPDATE
        }
    }
}
