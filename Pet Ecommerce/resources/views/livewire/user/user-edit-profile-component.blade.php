<div>
    <div class="container" style="padding: 30px 0">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Update Profile
                </div>
                <div class="panel-body">
                    @if(Session::has('message'))
                        <div class="alert alert-success" role="alert">{{Session::get('message')}}</div>
                    @endif
                    <form wire:submit.prevent="updateProfile">
                        <div class="col-md-4">
                            @if($newimage)
                                <img src="{{$newimage->temporaryUrl()}}" width="100%"/>                   
                            @elseif($image)
                            <img src="{{ asset('assets/images/profile') }}/{{ $image }}" width="100%">
                            @else
                                <img src="{{asset('assets/images/profile/default.jpg')}}" width="100%"/>
                            @endif
                            <input type="file" class="form-control" wire:model="newimage"/>
                        </div>
                        <a href="{{route('user.profile')}}" class="btn btn-info pull-right">Go Back</a>
                        <div class="col-md-8">
                            <p><b>Name: </b><input type="text" class="form-control" wire:model="name"/></p>
                            <p><b>Email: </b>{{$email}}</p>
                            <p><b>Phone: </b><input type="text" class="form-control" wire:model="mobile"/></p>
                            <hr>                        
                            <p><b>City: </b><input type="text" class="form-control" wire:model="city"/></p>                        
                            <p><b>Country: </b><input type="text" class="form-control" wire:model="country"/></p>
                            <br>
                            
                            <p><b>*Infromation that are provided below will not show in your profile but will be useful for us in case of delivery problems that will occur</b></p>
                            <p><b>Address line 1: </b><input type="text" class="form-control" wire:model="line1"/></p>
                            <p><b>Address line 2: </b><input type="text" class="form-control" wire:model="line2"/></p>
                            <p><b>Province/County: </b><input type="text" class="form-control" wire:model="province"/></p>
                            <p><b>Zipcode: <input type="text" class="form-control" wire:model="zipcode"/></b></p>
                            <button type="submit" class="btn btn-info pull-right">Update</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

