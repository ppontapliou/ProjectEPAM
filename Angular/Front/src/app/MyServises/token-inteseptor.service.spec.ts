import { TestBed } from '@angular/core/testing';

import { TokenInteseptorService } from './token-inteseptor.service';

describe('TokenInteseptorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TokenInteseptorService = TestBed.get(TokenInteseptorService);
    expect(service).toBeTruthy();
  });
});
