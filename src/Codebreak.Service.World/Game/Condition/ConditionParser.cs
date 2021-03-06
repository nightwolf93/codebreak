﻿using Codebreak.Framework.Generic;
using Codebreak.Service.World.Game.Entity;
using Codebreak.Service.World.Game.Spell;
using ExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Codebreak.Service.World.Game.Condition
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConditionParser : Singleton<ConditionParser>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, Func<ConditionScope, bool>> m_compiledExpressions;

        /// <summary>
        /// 
        /// </summary>
        public ConditionParser()
        {
            m_compiledExpressions = new Dictionary<string, Func<ConditionScope, bool>>();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool Check(string conditions, CharacterEntity character)
        {
            if (string.IsNullOrWhiteSpace(conditions))
                return true;

            Func<ConditionScope, bool> method;
            lock(m_compiledExpressions)
            {
                if (m_compiledExpressions.ContainsKey(conditions))
                {
                    method = m_compiledExpressions[conditions];
                }
                else
                {
                    if (conditions.Contains("BI")) // inutilisable
                    {
                        method = scope => false;
                    }
                    else
                    {
                        var hasTemplateRegex = new Regex(@"PO==(?<HasTemplate>[0-9]*)", RegexOptions.Compiled);
                        var notHasTemplateRegex = new Regex(@"PO!=(?<NotHasTemplate>[0-9]*)", RegexOptions.Compiled);

                        var subCond = hasTemplateRegex.Replace(conditions, @"character.Inventory.HasTemplate(${HasTemplate})");
                        subCond = notHasTemplateRegex.Replace(subCond, @"character.Inventory.NotHasTemplate(${NotHasTemplate})");

                        // Nouvelle
                        var realConditions = new StringBuilder(subCond);

                        // Stats tot
                        realConditions.Replace("CI", "character.Statistics.GetTotal(EffectEnum.AddIntelligence)");
                        realConditions.Replace("CV", "character.Statistics.GetTotal(EffectEnum.AddVitality)");
                        realConditions.Replace("CA", "character.Statistics.GetTotal(EffectEnum.AddAgility)");
                        realConditions.Replace("CW", "character.Statistics.GetTotal(EffectEnum.AddWisdom)");
                        realConditions.Replace("CC", "character.Statistics.GetTotal(EffectEnum.AddChance)");
                        realConditions.Replace("CS", "character.Statistics.GetTotal(EffectEnum.AddStrength)");

                        // Stats base
                        realConditions.Replace("Ci", "character.DatabaseRecord.Intelligence");
                        realConditions.Replace("Cs", "character.DatabaseRecord.Strength");
                        realConditions.Replace("Cv", "character.DatabaseRecord.Vitality");
                        realConditions.Replace("Ca", "character.DatabaseRecord.Agility");
                        realConditions.Replace("Cw", "character.DatabaseRecord.Wisdom");
                        realConditions.Replace("Cc", "character.DatabaseRecord.Chance");

                        // Perso
                        realConditions.Replace("Ps", "character.AlignmentId");
                        realConditions.Replace("Pa", "character.AlignmentPromotion");
                        realConditions.Replace("PP", "character.AlignmentLevel");
                        realConditions.Replace("PL", "character.Level");
                        realConditions.Replace("PK", "character.Inventory.Kamas");
                        realConditions.Replace("PG", "character.BreedId");
                        realConditions.Replace("PS", "character.Sex");
                        realConditions.Replace("PZ", "1"); // Abonné
                        realConditions.Replace("PN", "character.Name");
                        realConditions.Replace("PJ", "0"); // HasJob
                        realConditions.Replace("MK", "0"); // HasJob
                        realConditions.Replace("Pg", "0"); // Don
                        realConditions.Replace("PR", "0"); // Married
                        realConditions.Replace("PX", "character.Account.Power"); // Admin level
                        realConditions.Replace("PW", "10000"); // MaxWeight
                        realConditions.Replace("PB", "character.Map.SubAreaId");
                        realConditions.Replace("SI", "character.MapId");
                        realConditions.Replace("MiS", "character.Id");

                        var registry = new TypeRegistry();
                        registry.RegisterType("EffectEnum", typeof(EffectEnum));

                        method = new CompiledExpression<bool>(realConditions.ToString())
                        {
                            TypeRegistry = registry
                        }.ScopeCompile<ConditionScope>();
                    }
                    m_compiledExpressions.Add(conditions, method);
                }
            }
            return method(new ConditionScope(character));
        }

        private class ConditionScope
        {
            public CharacterEntity character
            {
                get;
                private set;
            }

            public ConditionScope(CharacterEntity ch)
            {
                character = ch;
            }
        }
    }
}
