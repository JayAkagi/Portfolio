<?php

namespace App\Http\Livewire\Admin;

use Livewire\Component;
use App\Models\HomeSlider;
use Carbon\Carbon;
use liveWire\WithFileUploads;

class AdminAddHomeSliderComponent extends Component
{
    use WithFileUploads;

    public $title;
    public $subtitle;
    public $price;
    public $link;
    public $image;
    public $status;

    public function mount()
    {
        $this->status = 0;
    }

    public function addSlide()
    {
        $this->validate([
            'title' => 'required',
            'image' => 'required|image|dimensions:width=1170,height=500',          
            'link' => 'required',
            'status' => 'required',
            
        ]);

        $slider = new HomeSlider();
        $slider->title = $this->title;
        $slider->subtitle = $this->subtitle;
        $slider->price = $this->price;
        $slider->link = $this->link;

        $imagename = Carbon::now()->timestamp. '.' .$this->image->extension();
        $this->image->storeAs('sliders',$imagename);
        
        $slider->image = $imagename;
        $slider->status = $this->status;

        $slider->save();

        session()->flash('message','Success! Slider has been created');
    }

    public function render()
    {
        return view('livewire.admin.admin-add-home-slider-component')->layout('layouts.base');
    }
}
