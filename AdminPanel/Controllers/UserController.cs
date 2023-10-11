﻿using AdminPanel.Models;
using CoreLayer.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName, 
                Email = u.Email,
                DisplayName = u.DisplayName,
                PhoneNumber = u.PhoneNumber,
                Roles =  _userManager.GetRolesAsync(u).Result
            }).ToListAsync();


            return View(users);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var AllRoles = await _roleManager.Roles.ToListAsync();
            var viewModel = new UserRoleViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles =  AllRoles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name =r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user,r.Name).Result

                }).ToList(),

            };
   
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel model /*,string id*/)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            //model.UserId = id;
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                //if (userRoles.Count() == 0 && role.IsSelected)
                //    await _userManager.AddToRoleAsync(user, role.Name);


                if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                
                if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.Name);
               
            }
   
            return RedirectToAction(nameof(Index));
        }
    }
}
