﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDKSWebServer.Models;

namespace PDKSWebServer.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IEnumerable<Article> Articles = new List<Article>()
        {
            new Article
            {
                ID = 1,
                Title = "Незабудка",
                Content = @"<div><h4 style='text-align:center'>Незабудка</h4><p></p><p>Більшість масонів і масонство в цілому особливо уважно ставляться до спадщини людства.І найдосвідченіший Майстер Масон, і молодий Підмайстер можуть оцінити уроки і знання,доступні нам завдяки ретельному навчанню. Наша повага до мудрості гуманітарних і точних наук дає нам змогу побачитиунікальні архітектурні основи і принципи побудови існуючого світу, а також отримати повагу до математичних чисел і букв.Крім того, ми, як і інші, бачимо внутрішню красу природнього світу, від величі сонця, зірок і місяця, до священної іурочистої символіки самої скромної рослини – акації. Бути масоном – значить володіти особливим даром, завжди шукати сенс,справжній або прихований, у навколишньому світі.</p><p>«Незабудка» – це неофіційна назва квітки Myosotis, зазвичай відомої як маленька квітка з п’ятьма синіми або фіолетовими пелюстками. Історично вона займає місце в поезії Водсворта і Торо, в середньовічних німецьких легендах, в християнської агіографії і англійської політичної історії. Для масонів незабудка – це символ, який нагадує нам про стійкість і опір, а також про любов до Братерства і до його принципів, навіть при незгодах і переслідуваннях.</p><p>З моменту свого створення в 1933 році нацистська Німеччина приділяла велику увагу пропаганді і досягненню позитивного суспільного сприйняття своїх ідеологічних цілей. Це включало правові, політичні та громадянські обмеження проти опонентів режиму і тих, хто став жертвою його жорстокої расової та соціальної ідеології. Поряд з євреями, гомосексуалістами, людьми із фізичними та розумовими вадами, католиками та  Свідками Єгови, масони стали об’єктом кримінального переслідування і виключення із суспільства. Це було досягнуто кількома способами. «Закон про дозвіл» 1933 року сприяв прийняттю у наступному році урядового указу, який офіційно розпустив всі масонські ложі у Третьому рейху, дозволив конфіскувати їх майно та офіційно заборонив членам, пов’язаним з масонством, вступати в нацистську партію. У 1934-5 роках Міністерство оборони ухвалило рішення, згідно якому солдати, офіцери і цивільний персонал армії не можуть бути членами масонських організацій, і Гітлер оголосив перемогу над «міжнародним єврейством», в якому він пов’язав антисемітизм з теоріями змови. Масони вважалися політичними в’язнями в німецькій системі концентраційних таборів, і тому були змушені носити значок перевернутого червоного трикутника.</p></div>",
                //Content = @"<div><p>NEW 222</p></div>",
                Author = new User
                {
                    ID = 1,
                    Username = "alksgest",
                    Password = "1111",
                    Role = new UserRole
                    {
                        ID = 1,
                        Role = UserRole.RoleType.Admin
                    }
                },
                CreationDate = DateTime.Now,
                Categories = new List<Category>
                {
                    new Category
                    {
                        ID = 1,
                        Title = "Контакти"
                    },
                    new Category
                    {
                        ID = 5,
                        Title = "Статті"
                    }
                }
            },
            new Article
            {
                ID = 2,
                Title = "Незабудка 2",
                Content = @"<div><p>NEW 222</p></div>",
                Author = new User
                {
                    ID = 1,
                    Username = "alksgest",
                    Password = "1111",
                    Role = new UserRole
                    {
                        ID = 1,
                        Role = UserRole.RoleType.Admin
                    },
                },
                CreationDate = DateTime.Now,
                Categories = new List<Category>
                {
                    new Category
                    {
                        ID = 1,
                        Title = "Контакти"
                    },
                }
            },
            new Article
            {
                ID = 3,
                Title = "Незабудка 3",
                Content = @"<div><p>NEW 224</p></div>",
                Author = new User
                {
                    ID = 1,
                    Username = "alksgest",
                    Password = "1111",
                    Role = new UserRole
                    {
                        ID = 1,
                        Role = UserRole.RoleType.Admin
                    }
                },
                CreationDate = DateTime.Now,
                Categories = new List<Category>
                {
                    new Category
                    {
                        ID = 5,
                        Title = "Статті"
                    }
                }
            }
        };

        public int AddArticle(Article article)
        {
            int nextId = 1 + Articles.Max(art => art.ID);
            article.ID = nextId;
            Articles.ToList().Add(article);
            return nextId;
        }

        public Article GetArticle(int id)
        {
            return this.Articles.SingleOrDefault(a => a.ID == id);
        }

        public IEnumerable<Article> GetArticles()
        {
            return this.Articles;
        }

        public IEnumerable<Article> GetArticles(int categoryId)
        {
            var result = new List<Article>();
            foreach(var art in this.Articles)
            {
                foreach(var cat in art.Categories)
                {
                    if(cat.ID == categoryId)
                    {
                        result.Add(art);
                        break;
                    }
                }
            }
            return result;
        }

        public void UpdateArticle(Article article)
        {

        }
    }
}