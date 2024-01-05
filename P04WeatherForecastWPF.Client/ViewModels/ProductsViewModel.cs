﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastWPF.Client.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ProductDetailsView _productDetailsView;

        [ObservableProperty]
        private ObservableCollection<Product> _products;


        [ObservableProperty]
        private Product _selectedProduct;


        public ProductsViewModel(IProductService productService, IMessageDialogService messageDialogService, 
            ProductDetailsView productDetailsView)
        {
            _productService = productService;
            _messageDialogService = messageDialogService;
            _productDetailsView = productDetailsView;
        }

        public async Task GetProductsAsync()
        {
            var result = await _productService.GetProductsAsync();
            if (result.Success)
            {
                Products = new ObservableCollection<Product>(result.Data);
            }
        }

        public async Task CreateProductAsync()
        {
            var newProduct = new Product
            {
                Title = _selectedProduct.Title,
                Price = _selectedProduct.Price,
                Description = _selectedProduct.Description,
                ReleaseDate = _selectedProduct.ReleaseDate,
            };

            var result = await _productService.CreateProductAsync(newProduct);
            if (result.Success)
            {
               await GetProductsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
           
        }

        public async Task DeleteProductAsync()
        {
            var result = await _productService.DeleteProductAsync(_selectedProduct.Id);
            if (result.Success)
            {
                await GetProductsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        public async Task UpdateProductAsync()
        {
            var result = await _productService.UpdateProductAsync(_selectedProduct);
            if (result.Success)
            {
                await GetProductsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        [RelayCommand]
        public async Task ShowDetails(Product product)
        {
            _productDetailsView.Show();
            SelectedProduct = product;
            _productDetailsView.DataContext = this;
        }


        [RelayCommand]
        public async Task Delete()
        {
            await _productService.DeleteProductAsync(_selectedProduct.Id);
            await GetProductsAsync();
        }
    }
}
