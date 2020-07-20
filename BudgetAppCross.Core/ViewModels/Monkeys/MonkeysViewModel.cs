﻿using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace BudgetAppCross.Core.ViewModels
{
    public class MonkeysViewModel : MvxViewModel
    {
        #region Fields
        IMvxNavigationService navigationService;
        #endregion

        #region Properties
        private ObservableCollection<Monkey> monkeys = new ObservableCollection<Monkey>();
        public ObservableCollection<Monkey> Monkeys
        {
            get { return monkeys; }
            set
            {
                SetProperty(ref monkeys, value);
            }
        }

        private ObservableCollection<Grouping<string, Monkey>> monkeysGrouped;
        public ObservableCollection<Grouping<string, Monkey>> MonkeysGrouped
        {
            get { return monkeysGrouped; }
            set
            {
                SetProperty(ref monkeysGrouped, value);
            }
        }

        #endregion

        #region Constructors
        public MonkeysViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;

            

            Monkeys.Add(new Monkey
            {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
            });

            Monkeys.Add(new Monkey
            {
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
            });

            Monkeys.Add(new Monkey
            {
                Name = "Blue Monkey",
                Location = "Central and East Africa",
                Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
            });


            Monkeys.Add(new Monkey
            {
                Name = "Squirrel Monkey",
                Location = "Central & South America",
                Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.",
            });

            Monkeys.Add(new Monkey
            {
                Name = "Golden Lion Tamarin",
                Location = "Brazil",
                Details = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
            });

            Monkeys.Add(new Monkey
            {
                Name = "Howler Monkey",
                Location = "South America",
                Details = "Howler monkeys are among the largest of the New World monkeys. Fifteen species are currently recognised. Previously classified in the family Cebidae, they are now placed in the family Atelidae.",
            });

            Monkeys.Add(new Monkey
            {
                Name = "Japanese Macaque",
                Location = "Japan",
                Details = "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey because they live in areas where snow covers the ground for months each",
            });

            Monkeys.Add(new Monkey
            {
                Name = "Mandrill",
                Location = "Southern Cameroon, Gabon, Equatorial Guinea, and Congo",
                Details = "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill. It is found in southern Cameroon, Gabon, Equatorial Guinea, and Congo.",
            });

            Monkeys.Add(new Monkey
            {
                Name = "Proboscis Monkey",
                Location = "Borneo",
                Details = "The proboscis monkey or long-nosed monkey, known as the bekantan in Malay, is a reddish-brown arboreal Old World monkey that is endemic to the south-east Asian island of Borneo.",
            });


            var sorted = from monkey in Monkeys
                         orderby monkey.Name
                         group monkey by monkey.NameSort into monkeyGroup
                         select new Grouping<string, Monkey>(monkeyGroup.Key, monkeyGroup);

            MonkeysGrouped = new ObservableCollection<Grouping<string, Monkey>>(sorted);
        }
        #endregion

        #region Methods

        #endregion



    }
}
