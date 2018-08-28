import { TestBed, inject } from '@angular/core/testing';

import { CarreraService } from './carrera.service';

describe('CarreraService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CarreraService]
    });
  });

  it('should be created', inject([CarreraService], (service: CarreraService) => {
    expect(service).toBeTruthy();
  }));
});
