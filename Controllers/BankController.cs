using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bank_proj.Models;
using System.Linq;

namespace bank_proj.Controllers
{
    public class BankController : Controller
    {
        private BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("account/{id}")]
        public IActionResult Account(int id)
        {
            string exceed = HttpContext.Session.GetString("exceed");
            int? currId = HttpContext.Session.GetInt32("currId");
            if(currId != id)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            User currUser = _context.users.SingleOrDefault(user => user.userid == currId);
            ViewBag.user = currUser;
            List<Transaction> allTrans = _context.transactions.Where(trans => trans.userid == currId).OrderByDescending(t => t.transactionid).ToList();
            ViewBag.trans = allTrans;
            ViewBag.exceed = TempData["exceed"];
            return View();
        }

        [HttpPost]
        [Route("transaction")]
        public IActionResult Transaction(float amount, int id)
        {
            User currUser = _context.users.SingleOrDefault(user => user.userid == id);
            float balance = currUser.balance;
            if((balance += amount) < 0)
            {
                TempData["exceed"] = "Cannot withdraw more than Current Balance";
                return Redirect($"/account/{currUser.userid}");
            }

            Transaction newTrans = new Transaction
            {
                amount = amount,
                userid = id,
                date = DateTime.Now
            };
            _context.transactions.Add(newTrans);
            _context.SaveChanges();
            
            currUser.balance += amount;
            _context.SaveChanges();
            return Redirect($"/account/{currUser.userid}");
        }
    }
}