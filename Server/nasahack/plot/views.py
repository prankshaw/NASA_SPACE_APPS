from django.http import HttpResponse, JsonResponse
#from matplotlib import pylab
#from pylab import *
#import PIL, PIL.Image
#import io
from plot.longilati import smallvalues
from plot.displacenearest import smallvalues as svdis
from plot.accodnearest import smallvalues as svaccod
from plot.populationnearest import smallvalues as svpop
from plot.refugee import smallvalues as refsv

def test(request):
    longt = request.GET.get('lon', 27.999912)
    lat = request.GET.get('lat', 72.567)   
    x=smallvalues(lat, longt)
    #print(x, lat, longt)
    return HttpResponse(x)

def svdisp(request):
    longt = request.GET.get('lon', 27.999912)
    lat = request.GET.get('lat', 72.567)   
    x=svdis(float(lat), float(longt))
    #print(x, lat, longt)
    return HttpResponse(x)

def accoddisp(request):
    longt = request.GET.get('lon', 27.999912)
    lat = request.GET.get('lat', 72.567)   
    x=svaccod(float(lat), float(longt))
    #print(x, lat, longt)
    return HttpResponse(x)

def popdisp(request):
    longt = request.GET.get('lon', 27.999912)
    lat = request.GET.get('lat', 72.567)   
    x=svpop(float(lat), float(longt))
    #print(x, lat, longt)
    return HttpResponse(x)

def refs(request):
    longt = request.GET.get('lon', 27.999912)
    lat = request.GET.get('lat', 72.567)   
    x=refsv(float(lat), float(longt))
    #print(x, lat, longt)
    return HttpResponse(x)
