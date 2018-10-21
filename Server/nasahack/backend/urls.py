
from django.contrib import admin
from django.urls import path, include
from watersimulation.views import *
urlpatterns = [
    path('admin/', admin.site.urls),
    path('plot/', include('plot.urls')),
    path('wsg/', val_get),
    path('wsp/', val_put),
    path('wss/', start_pred)
]
