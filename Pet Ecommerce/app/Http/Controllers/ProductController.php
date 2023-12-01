<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

class ProductController extends Controller
{
    public function showShop()
    {
        $products = Product::all();

        return view('shop-component', ['products' => $products]);
    }
}
