﻿using Codebreak.Service.World.Database.Structures;
using Codebreak.Service.World.Game.Action;
using Codebreak.Service.World.Game.Database.Repository;
using Codebreak.Service.World.Game.Exchange;
using Codebreak.Service.World.Game.Fight;
using Codebreak.Service.World.Game.Spell;
using Codebreak.Service.World.Game.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebreak.Service.World.Manager;
using Codebreak.Service.World.Network;

namespace Codebreak.Service.World.Game.Entity
{
    public sealed class CharacterEntity : FighterBase
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                return DatabaseRecord.Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int MapId
        {
            get
            {
                return DatabaseRecord.MapId;
            }
            set
            {
                DatabaseRecord.MapId = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public override int CellId
        {
            get
            {
                return DatabaseRecord.CellId;
            }
            set
            {
                DatabaseRecord.CellId = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public override int Level
        {
            get
            {
                return DatabaseRecord.Level;
            }
            set
            {
                DatabaseRecord.Level = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Restriction
        {
            get
            {
                return DatabaseRecord.Restriction;
            }
            set
            {
                DatabaseRecord.Restriction = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AlignmentId
        {
            get
            {
                return _alignmentRecord.AlignmentId;
            }
            set
            {
                _alignmentRecord.AlignmentId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AlignmentLevel
        {
            get
            {
                return _alignmentRecord.Level;
            }
            set
            {
                _alignmentRecord.Level = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<InventoryItemDAO> Items
        {
            get
            {
                return DatabaseRecord.GetItems();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AlignmentPromotion
        {
            get
            {
                return _alignmentRecord.Promotion;
            }
            set
            {
                _alignmentRecord.Promotion = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AlignmentHonour
        {
            get
            {
                return _alignmentRecord.Honour;
            }
            set
            {
                _alignmentRecord.Honour = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AlignmentDishonour
        {
            get
            {
                return _alignmentRecord.Dishonour;
            }
            set
            {
                _alignmentRecord.Dishonour = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long Kamas
        {
            get
            {
                return DatabaseRecord.Kamas;
            }
            set
            {
                DatabaseRecord.Kamas = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CaractPoint
        {
            get
            {
                return DatabaseRecord.CaracPoint;
            }
            set
            {
                DatabaseRecord.CaracPoint = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SpellPoint
        {
            get
            {
                return DatabaseRecord.SpellPoint;
            }
            set
            {
                DatabaseRecord.SpellPoint = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long Experience
        {
            get
            {
                return DatabaseRecord.Experience;
            }
            set
            {
                DatabaseRecord.Experience = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long ExperienceFloorCurrent
        {
            get
            {
                return ExperienceManager.Instance.GetFloor(Level, ExperienceTypeEnum.CHARACTER);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long ExperienceFloorNext
        {
            get
            {
                return ExperienceManager.Instance.GetFloor(Level + 1, ExperienceTypeEnum.CHARACTER); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int RealLife
        {
            get
            {
                return DatabaseRecord.Life;
            }
            set
            {
                DatabaseRecord.Life = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Energy
        {
            get
            {
                return DatabaseRecord.Energy;
            }
            set
            {
                DatabaseRecord.Energy = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int BaseLife
        {
            get
            {
                return 50 + (Level * 5);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string HexColor1
        {
            get
            {
                return DatabaseRecord.GetHexColor1();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string HexColor2
        {
            get
            {
                return DatabaseRecord.GetHexColor2();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string HexColor3
        {
            get
            {
                return DatabaseRecord.GetHexColor3();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int SkinBase
        {
            get
            {
                return DatabaseRecord.Skin;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int SkinSizeBase
        {
            get
            {
                return DatabaseRecord.SkinSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public CharacterBreedEnum Breed
        {
            get
            {
                return (CharacterBreedEnum)DatabaseRecord.Breed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Sex
        {
            get
            {
                return DatabaseRecord.Sex ? 1 : 0;
            }
            set
            {
                DatabaseRecord.Sex = value == 1 ? true : false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Dead
        {
            get
            {
                return DatabaseRecord.Dead;
            }
            set
            {
                DatabaseRecord.Dead = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public CharacterDAO DatabaseRecord
        {
            get;
            private set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Aura
        {
            get
            {
                if (Level > 199)
                    return 2;
                else if (Level > 100)
                    return 1;
                return 0;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override bool TurnReady
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool TurnPass
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        private CharacterAlignmentDAO _alignmentRecord;

        public CharacterEntity(CharacterDAO characterDAO)
            : base(EntityTypEnum.TYPE_CHARACTER, characterDAO.Id)
        {
            _alignmentRecord = characterDAO.GetAlignment();

            DatabaseRecord = characterDAO;
            Statistics = new GenericStats(characterDAO);
            Inventory = new CharacterInventory(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <param name="remoteEntity"></param>
        public override void DispatchChatMessage(ChatChannelEnum channel, string message, EntityBase remoteEntity = null)
        {
            switch(channel)
            {
                case ChatChannelEnum.CHANNEL_GENERAL:
                    if (message.StartsWith(".item"))
                    {
                        var cmdData = message.Split(' ');
                        var templateId = int.Parse(cmdData[1]);

                        var itemTemplate = ItemTemplateRepository.Instance.GetTemplate(templateId);
                        if (itemTemplate != null)
                        {
                            var instance = itemTemplate.CreateItem(1, ItemSlotEnum.SLOT_INVENTORY, true);
                            if (instance != null)
                                Inventory.AddItem(instance);
                        }
                        return;
                    }
                    else if (message.StartsWith(".200"))
                    {
                        while (Level != 200)
                        {
                            LevelUp();
                        }

                        base.Dispatch(WorldMessage.CHARACTER_NEW_LEVEL(Level));
                        base.Dispatch(WorldMessage.SPELLS_LIST(Spells));
                        base.Dispatch(WorldMessage.ACCOUNT_STATS(this));
                        return;
                    }
                    else if (message.StartsWith(".save"))
                    {
                        WorldService.Instance.UpdateWorld();
                        return;
                    }                    
                    break;
            }
            base.DispatchChatMessage(channel, message, remoteEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void ExchangePlayer(CharacterEntity player)
        {
            CurrentAction = new GamePlayerExchangeAction(this, player);
            player.CurrentAction = CurrentAction;

            StartAction(GameActionTypeEnum.EXCHANGE);
            player.StartAction(GameActionTypeEnum.EXCHANGE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defender"></param>
        public void ChallengePlayer(CharacterEntity player)
        {
            CurrentAction = new GameChallengeRequestAction(this, player);
            player.CurrentAction = CurrentAction;

            StartAction(GameActionTypeEnum.CHALLENGE_REQUEST);
            player.StartAction(GameActionTypeEnum.CHALLENGE_REQUEST);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void ExchangeShop(EntityBase entity)
        {
            CurrentAction = new GameShopExchangeAction(this, entity);

            StartAction(GameActionTypeEnum.EXCHANGE);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="experience"></param>
        public void AddExperience(long experience)
        {
            Experience += experience;

            var currentLevel = Level;

            if (Experience > ExperienceFloorNext)
            {
                do
                {
                    LevelUp();
                }
                while (Experience > ExperienceFloorNext && ExperienceFloorNext != -1);

                base.Dispatch(WorldMessage.CHARACTER_NEW_LEVEL(Level));
            }

            if (Level != currentLevel)
            {
                base.Dispatch(WorldMessage.SPELLS_LIST(Spells));
                base.Dispatch(WorldMessage.ACCOUNT_STATS(this));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LevelUp()
        {
            Level++;
            SpellPoint++;
            CaractPoint += 5;
            Life = MaxLife;

            if (Level == 100)
            {
                DatabaseRecord.Ap += 1;
                Statistics.AddBase(EffectEnum.AddAP, 1);
            }

            if (Spells != null)
            {
                Spells.GenerateLevelUpSpell(Breed, Level);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeType"></param>
        /// <returns></returns>
        public override bool CanBeExchanged(ExchangeTypeEnum exchangeType)
        {
            return base.CanBeExchanged(exchangeType) && exchangeType == ExchangeTypeEnum.EXCHANGE_PLAYER;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="message"></param>
        public override void SerializeAs_GameMapInformations(OperatorEnum operation, StringBuilder message)
        {
            switch (operation)
            {
                case OperatorEnum.OPERATOR_REMOVE:
                    message.Append(Id);
                    break;

                case OperatorEnum.OPERATOR_ADD:
                case OperatorEnum.OPERATOR_REFRESH:
                    if (HasGameAction(GameActionTypeEnum.MAP))
                    {
                        message.Append(CellId).Append(';');
                        message.Append(Orientation).Append(';'); ;
                        message.Append((int)Type).Append(';');
                        message.Append(Id).Append(';');
                        message.Append(Name).Append(';');
                        message.Append((int)Breed).Append(';');
                        message.Append(SkinBase).Append('^');
                        message.Append(SkinSizeBase).Append(';');
                        message.Append(Sex).Append(';');
                        message.Append("0,0,0,0"); // AlignmentInfos
                        message.Append(';');
                        message.Append(HexColor1).Append(';');
                        message.Append(HexColor2).Append(';');
                        message.Append(HexColor3).Append(';');
                        Inventory.SerializeAs_ActorLookMessage(message);
                        message.Append(';');
                        message.Append(Aura).Append(';');
                        message.Append("").Append(';'); // DisplayEmotes
                        message.Append("").Append(';'); // EmotesTimer
                        //if (this.HasGuild())
                        //{
                        //    message.Append(this.GetGuild().Name).Append(';');
                        //    message.Append(this.GetGuild().Emblem).Append(';');
                        //}
                        //else
                        //{
                        message.Append("").Append(';'); // GuildInfos
                        message.Append("").Append(';');
                        //}
                        message.Append(Util.EncodeBase36(EntityRestriction))
                            .Append(';');
                        message.Append("")
                            .Append(';'); // MountLightInfos
                    }
                    else if (HasGameAction(GameActionTypeEnum.FIGHT))
                    {
                        message.Append(Cell.Id).Append(';');
                        message.Append(Orientation).Append(';'); // Direction
                        message.Append((int)Type).Append(';');
                        message.Append(Id).Append(';');
                        message.Append(Name).Append(';');
                        message.Append((int)Breed).Append(';');
                        message.Append(Skin).Append('^');
                        message.Append(SkinSize).Append(';');
                        message.Append(Sex).Append(';');
                        message.Append(Level).Append(';');
                        message.Append("0,0,0,0").Append(';'); // Alignmentnfos
                        message.Append(HexColor1).Append(';');
                        message.Append(HexColor2).Append(';');
                        message.Append(HexColor3).Append(';');
                        Inventory.SerializeAs_ActorLookMessage(message);
                        message.Append(';');
                        message.Append(Life).Append(';');
                        message.Append(AP).Append(';');
                        message.Append(MP).Append(';');
                        switch (Fight.Type)
                        {
                            case FightTypeEnum.TYPE_CHALLENGE:
                            case FightTypeEnum.TYPE_AGGRESSION:
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentNeutral) + Statistics.GetTotal(EffectEnum.AddReduceDamagePercentPvPNeutral)).Append(';');
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentEarth) + Statistics.GetTotal(EffectEnum.AddReduceDamagePercentPvPEarth)).Append(';');
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentFire) + Statistics.GetTotal(EffectEnum.AddReduceDamagePercentPvPFire)).Append(';');
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentWater) + Statistics.GetTotal(EffectEnum.AddReduceDamagePercentPvPWater)).Append(';');
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentAir) + Statistics.GetTotal(EffectEnum.AddReduceDamagePercentPvPAir)).Append(';');
                                break;

                            default:
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentNeutral)).Append(';');
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentEarth)).Append(';');
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentFire)).Append(';');
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentWater)).Append(';');
                                message.Append(Statistics.GetTotal(EffectEnum.AddReduceDamagePercentAir)).Append(';');
                                break;
                        }
                        message.Append(Statistics.GetTotal(EffectEnum.AddAPDodge)).Append(';');
                        message.Append(Statistics.GetTotal(EffectEnum.AddMPDodge)).Append(';');
                        message.Append(Team.Id).Append(';');
                        message.Append("").Append(';'); // TODO Display Paddock
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void SerializeAs_ShopItemsListInformations(StringBuilder message)
        {
            throw new NotImplementedException();
        }
    }
}
