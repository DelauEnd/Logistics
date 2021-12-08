using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using Entities.Models.OwnedModels;
using Entities.Utility;
using Logistics.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Logistics.AdminForms
{
    /// <summary>
    /// Логика взаимодействия для CreateUserForm.xaml
    /// </summary>
    public partial class CreateUserForm : ExtendedWindow
    {
        private bool ForUpdate { get; set; }
        public bool Created { get; private set; } = false;
        public Guid? UserId { get; set; }
        private UserRole Role { get; set; }

        public CreateUserForm(bool forUpdate = false)
        {
            InitializeComponent();
            this.SetupWindowStyle();
            this.ForUpdate = forUpdate;
            if (ForUpdate)
                accountBox.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragWindow(e);
        }

        private void DragWindow(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnResizeWindowsClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void SetUserInfo(UserWithLoginDto user)
        {
            UserId = user.Id;
            Role = (UserRole)Enum.Parse(typeof(UserRole), user.Role);
            CheckRole();

            userName.Text = user.ContactPerson.Name;
            userPatronymic.Text = user.ContactPerson.Patronymic;
            userSurname.Text = user.ContactPerson.Surname;
            userPhoneNum.Text = user.ContactPerson.PhoneNumber;
        }

        private void CheckRole()
        {
            if (Role == UserRole.Admin)
                adminCheck.IsChecked = true;
            else if (Role == UserRole.Logist)
                logistCheck.IsChecked = true;
        }

        private UserRole GetRole()
        {
            var role = adminCheck.IsChecked == true ?
                UserRole.Admin :
                UserRole.Logist;
            return role;
        }

        public async Task BuildUser()
        {
            try
            {
                await TryToBuildUser();
            }
            catch (Exception e)
            {
                HandleSaveExcetion(e);
            }
            await repository.SaveAsync();
        }

        private async Task TryToBuildUser()
        {
            if (ForUpdate)
            {
                var user = GetUpdatedUser();
                await UpdateUser(user);
            }
            else
            {
                var user = GetCreatedUser();
                var mappedUser = MapUser(user);
                repository.Users.CreateUser(mappedUser);
            }
        }

        private User MapUser(UserForCreationDto user)
        {
            var mapped = new User
            {
                Id = user.Id,
                AccountInfo = new AccountInfo
                {
                    Login = user.Login,
                    Password = user.Password
                },
                ContactPerson = user.ContactPerson,
                Role = user.Role
            };
            mapped.AccountInfo.InitPasswordHash();
            return mapped;
        }

        private async Task UpdateUser(UserForUpdateDto user)
        {
            var userToUpdate = await repository.Users.GetUserByIdAsync(user.Id, true);
            userToUpdate.Role = user.Role;
            userToUpdate.ContactPerson = user.ContactPerson;

            repository.Users.UpdateUser(userToUpdate);
        }

        private UserForUpdateDto GetUpdatedUser()
        {
            Guid id = (Guid)GetCustomerId();
            UserRole role = GetRole();

            var user = new UserForUpdateDto
            {
                Id = id,
                ContactPerson = new Person
                {
                    Name = userName.Text,
                    Patronymic = userPatronymic.Text,
                    Surname = userSurname.Text,
                    PhoneNumber = userPhoneNum.Text
                },
                Role = role
            };
            return user;
        }

        private UserForCreationDto GetCreatedUser()
        {
            Guid id = (Guid)GetCustomerId();
            UserRole role = GetRole();

            var user = new UserForCreationDto
            {
                Id = id,
                ContactPerson = new Person
                {
                    Name = userName.Text,
                    Patronymic = userPatronymic.Text,
                    Surname = userSurname.Text,
                    PhoneNumber = userPhoneNum.Text
                },
                Role = role,
                Login = login.Text,
                Password = password.Text
            };
            return user;
        }

        private Guid? GetCustomerId()
        {
            if (UserId == null)
                UserId = Guid.NewGuid();

            return UserId;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Created = true;
            Close();
        }
    }
}
