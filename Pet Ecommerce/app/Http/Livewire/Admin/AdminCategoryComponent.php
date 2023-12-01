<?php

namespace App\Http\Livewire\Admin;

use Livewire\Component;
use App\Models\Category;
use LiveWire\WithPagination;
use App\Models\Subcategory;

class AdminCategoryComponent extends Component
{
    use WithPagination;

    public function deleteCategory($id)
    {
        $category = Category::find($id);
        $category->delete();
        session()->flash('message','Success! Category has been deleted');
    }

    public function deleteSubcategory($id)
    {
        $subcategory = Subcategory::find($id);
        $subcategory->delete();
        session()->flash('message','Subcategory has been deleted');
    }

    public function render()
    {       
        $categories = Category::paginate(5);
        return view('livewire.admin.admin-category-component',['categories'=>$categories])->layout('layouts.base');
    }
}
