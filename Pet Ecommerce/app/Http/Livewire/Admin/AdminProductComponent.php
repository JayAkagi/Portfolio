<?php

namespace App\Http\Livewire\Admin;

use Livewire\Component;
use App\Models\Product;
use Livewire\WithPagination;

class AdminProductComponent extends Component
{
    use WithPagination;
    public $searchTerm;

    public function deleteProduct($id)
    {
        $product = Product::find($id);
        if($product->image)
        {
            unlink('assets/images/products/'.$product->image);
        }
        if($product->images)
        {
            $images = explode(',',$product->images);
            foreach($images as $image)
            {
                if($image)
                {
                    unlink('assets/images/products/'.$image);
                }
                
            }
        }
        $product->delete();
        session()->flash('message','Success! Product has been deleted');
    }
    public function render()
    {
        $search = '%' . $this->searchTerm . '%';
        $products = Product::where('name','LIKE',$search)
        ->orWhere('stock_status', 'LIKE', $search)
        ->orWhere('regular_price', 'LIKE', $search)
        ->orWhere('sale_price','Like',$search)
        ->orderBy('id','DESC')
        ->paginate(10);
        return view('livewire.admin.admin-product-component',['products'=>$products])->layout('layouts.base');
    }
}
