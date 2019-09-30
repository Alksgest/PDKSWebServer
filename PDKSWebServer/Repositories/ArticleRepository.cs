using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDKSWebServer.DbContexts;
using PDKSWebServer.Models;

namespace PDKSWebServer.Repositories
{
    public class ArticleRepository : IArticleRepository, IDisposable
    {
        private bool disposed = false;

        private readonly MainContext _db = new MainContext();
        //private readonly List<Article> Articles = new List<Article>()
        //{
        //    new Article
        //    {
        //        ArticleID = 1,
        //        Title = "Незабудка",
        //        Content = @"<div><h4 style='text-align:center'>Незабудка</h4><p></p><p>Більшість масонів і масонство в цілому особливо уважно ставляться до спадщини людства.І найдосвідченіший Майстер Масон, і молодий Підмайстер можуть оцінити уроки і знання,доступні нам завдяки ретельному навчанню. Наша повага до мудрості гуманітарних і точних наук дає нам змогу побачитиунікальні архітектурні основи і принципи побудови існуючого світу, а також отримати повагу до математичних чисел і букв.Крім того, ми, як і інші, бачимо внутрішню красу природнього світу, від величі сонця, зірок і місяця, до священної іурочистої символіки самої скромної рослини – акації. Бути масоном – значить володіти особливим даром, завжди шукати сенс,справжній або прихований, у навколишньому світі.</p><p>«Незабудка» – це неофіційна назва квітки Myosotis, зазвичай відомої як маленька квітка з п’ятьма синіми або фіолетовими пелюстками. Історично вона займає місце в поезії Водсворта і Торо, в середньовічних німецьких легендах, в християнської агіографії і англійської політичної історії. Для масонів незабудка – це символ, який нагадує нам про стійкість і опір, а також про любов до Братерства і до його принципів, навіть при незгодах і переслідуваннях.</p><p>З моменту свого створення в 1933 році нацистська Німеччина приділяла велику увагу пропаганді і досягненню позитивного суспільного сприйняття своїх ідеологічних цілей. Це включало правові, політичні та громадянські обмеження проти опонентів режиму і тих, хто став жертвою його жорстокої расової та соціальної ідеології. Поряд з євреями, гомосексуалістами, людьми із фізичними та розумовими вадами, католиками та  Свідками Єгови, масони стали об’єктом кримінального переслідування і виключення із суспільства. Це було досягнуто кількома способами. «Закон про дозвіл» 1933 року сприяв прийняттю у наступному році урядового указу, який офіційно розпустив всі масонські ложі у Третьому рейху, дозволив конфіскувати їх майно та офіційно заборонив членам, пов’язаним з масонством, вступати в нацистську партію. У 1934-5 роках Міністерство оборони ухвалило рішення, згідно якому солдати, офіцери і цивільний персонал армії не можуть бути членами масонських організацій, і Гітлер оголосив перемогу над «міжнародним єврейством», в якому він пов’язав антисемітизм з теоріями змови. Масони вважалися політичними в’язнями в німецькій системі концентраційних таборів, і тому були змушені носити значок перевернутого червоного трикутника.</p></div>",
        //        //Content = @"<div><p>NEW 222</p></div>",
        //        Author = new User
        //        {
        //            UserId = 1,
        //            Username = "alksgest",
        //            Password = "1111",
        //            Role = "admin"
        //        },
        //        CreationDate = DateTime.Now,
        //        Categories = new List<Category>
        //        {
        //            new Category
        //            {
        //                CategoryId = 1,
        //                Title = "Контакти"
        //            },
        //            new Category
        //            {
        //                CategoryId = 5,
        //                Title = "Статті"
        //            }
        //        }
        //    }
        //}
    

        public int AddArticle(Article article)
        {
            article.Author = _db.Users
                .FirstOrDefault(u => u.ID == article.Author.ID);

            article.Category = _db.Categories
                .FirstOrDefault(cat => cat.ID == article.Category.ID);

            _db.Articles.Add(article);
            var res = _db.SaveChanges();
            return res;
        }

        public Article GetArticle(int id)
        {
            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category)
                .SingleOrDefault(a => a.ID == id);
            //return _db.Articles.Include(article => article.Author)
            //    .Include(article => article.ArticleCategories)
            //    .ThenInclude(ac => ac.Category)
            //    .SingleOrDefault(a => a.ArticleID == id);
            //return this.Articles.SingleOrDefault(a => a.ArticleID == id);
        }

        public IEnumerable<Article> GetArticles()
        {
            //var articles = _db.Articles
            //    .Include(article => article.Author)
            //    .Include(article => article.ArticleCategories)
            //    .ThenInclude(ac => ac.Category);
            //return articles;
            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category);
        }

        public IEnumerable<Article> GetArticles(int categoryId)
        {
            //var result = new List<Article>();
            //foreach(var art in this.Articles)
            //{
            //    foreach(var cat in art.Categories)
            //    {
            //        if(cat.CategoryId == categoryId)
            //        {
            //            result.Add(art);
            //            break;
            //        }
            //    }
            //}
            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category)
                .Where(article => article.Category.ID == categoryId); 
        }

        public void UpdateArticle(Article article)
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _db.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
